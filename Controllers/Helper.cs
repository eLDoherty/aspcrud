using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using learnnet.Models;
using System.Configuration;
using System.Data;


namespace learnnet.Controllers
{
    public class Helper : Controller
    {

        public List<Product> GetData()
        {
            List<Product> ProductList = new List<Product>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM products", con)
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

    }
}