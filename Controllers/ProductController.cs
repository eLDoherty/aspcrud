using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using learnnet.Models;
using System.Configuration;
using System.Data;

namespace learnnet.Controllers
{
    public class ProductController : Controller
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

        public ActionResult Index()
        {
            var data = GetData();
            return View(data);
        }


        //Create Product Handler
        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Product prd)
        {
            if (ModelState.IsValid)
            {
                var product = GetData() != null ? GetData().Where(s => s.name == prd.name).FirstOrDefault() : null;

                if (product != null)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }
                prd.thumbnail = "https://source.unsplash.com/random/350x350?sig=" + product.id; 
                GetData().Add(prd);
                return RedirectToAction("Index");
            }
            return View(prd);
        }

        // Edit product
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            var prod = GetData().Where(s => s.id == id).FirstOrDefault();
            return View(prod);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Product prd)
        {
            if (ModelState.IsValid)
            {
                var editedProduct = GetData().Where(s => s.name == prd.name).FirstOrDefault();
                bool productAlreadyExist = editedProduct == null ? false : true;

                if (productAlreadyExist && editedProduct.id != prd.id)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                var product = GetData().Where(s => s.id == prd.id).FirstOrDefault();
                GetData().Remove(product);
                prd.thumbnail = "https://source.unsplash.com/random/350x350?sig=" + prd.id;
                GetData().Add(prd);

                return RedirectToAction("Index");
            }
            return View(prd);
        }

        // Delete product
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            var product = GetData().Where(s => s.id == id).FirstOrDefault();
            GetData().Remove(product);
            return RedirectToAction("Index");
        }
    }
}