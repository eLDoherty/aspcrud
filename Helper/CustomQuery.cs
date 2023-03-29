using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using learnnet.Models;

namespace learnnet.Helper
{
    public class CustomQuery : Controller
    {

        // Retrieve all data in db
        public static List<Product> GetData()
        {
            List<Product> ProductList = new List<Product>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.products", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var prd = new Product
                    {
                        name = rdr["name"].ToString(),
                        price = Convert.ToInt32(rdr["price"]),
                        slug = rdr["slug"].ToString(),
                        thumbnail = rdr["thumbnail"].ToString(),
                    };

                    ProductList.Add(prd);
                }
            }
            return ProductList;
        }

        // Insert data to DB
        public static bool InsertData(string name, decimal price, string thumbnail)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                con.Open();
                var query = "INSERT INTO dbo.products (name ,price ,slug ,thumbnail) VALUES ('"+name+"','"+price+"','"+Slugify(name)+"','"+thumbnail+"')";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Find data by ID
        public static Product ChooseData(int id)
        {
            return GetData().Where(data => data.id == id).FirstOrDefault();
        }

        // Edit data
        public static bool EditData(int id)
        {
            return true;
        }




        // Utility -- should create on another class?
        public static string Slugify(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            var stringBuilder = new StringBuilder();
            foreach (char c in input.ToArray())
            {
                if (Char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
                else if (c == ' ')
                {
                    stringBuilder.Append("-");
                }
            }

            return stringBuilder.ToString().ToLower();
        }
    }
}