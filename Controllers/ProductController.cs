using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using learnnet.Models;

namespace learnnet.Controllers
{
    public class ProductController : Controller
    {
        public IList<Product> ProductList = new List<Product>
        {
               new Product() { ProductId = 1, Name = "Product 1", Price = 18 } ,
               new Product() { ProductId = 2, Name = "Product 2", Price = 19 } ,
               new Product() { ProductId = 3, Name = "Product 3", Price = 20 } ,
        };

        public ActionResult Index()
        {
            ViewBag.Message = "Product page is created!";

            return View(ProductList);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int Id)
        {
            var prod = ProductList.Where(s => s.ProductId == Id).FirstOrDefault();
            return View(prod);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Product prd)
        {
            var product = ProductList.Where(s => s.ProductId == prd.ProductId).FirstOrDefault();
            ProductList.Remove(product);
            ProductList.Add(prd);

            return RedirectToAction("Index");
        }

    }
}