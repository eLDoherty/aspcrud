using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                        id = Convert.ToInt32(rdr["id"]),
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
        public static bool InsertData(string name, decimal price)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                var thumbnail = GetRandomImageURL();
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

        //  Edit data in DB
        public static bool EditData(Product prd)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                var thumbnail = GetRandomImageURL();
                con.Open();
                var query = "UPDATE dbo.products SET name = '"+prd.name+"',price = '"+prd.price+"' ,slug = '"+Slugify(prd.name)+"' ,thumbnail = '"+thumbnail+"' WHERE id=" + prd.id;
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();
                        if(result > 0)
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

        // Delete data by ID
        public static bool DeletData(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.products WHERE id=" + id;
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();
                        if(result > 0)
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

        /* 
         * Utility -- should create on another class?
         * 
         * Slugify string
         */
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

        // Get unsplash random image
        public static string GetRandomImageURL()
        {
            Random rnd = new Random();
            int sig = rnd.Next(1, 13);
            var thumbnail = "https://source.unsplash.com/random/350x350?sig=" + sig;
            return thumbnail;
        }
    }
}