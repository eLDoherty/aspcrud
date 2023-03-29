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
using learnnet.Helper;

namespace learnnet.Controllers
{
    public class ProductController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();

        public ActionResult Index()
        {
            var data = CustomQuery.GetData();
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
                var product = CustomQuery.GetData()?.Where(s => s.name == prd.name).FirstOrDefault();
                Random rnd = new Random();
                int sig = rnd.Next(1, 13);

                if (product != null)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                var thumbnail = "https://source.unsplash.com/random/350x350?sig=" + sig; 

                var pushData =  CustomQuery.InsertData(prd.name, prd.price, thumbnail);
                if (pushData)
                {
                    return RedirectToAction("Index");
                }
               
            }
            return View(prd);
        }

        // Edit product
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            var prod = CustomQuery.GetData().Where(s => s.id == id).FirstOrDefault();
            return View(prod);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Product prd)
        {
            if (ModelState.IsValid)
            {
                var editedProduct = CustomQuery.GetData().Where(s => s.name == prd.name).FirstOrDefault();
                bool productAlreadyExist = editedProduct == null ? false : true;

                if (productAlreadyExist && editedProduct.id != prd.id)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                var product = CustomQuery.GetData().Where(s => s.id == prd.id).FirstOrDefault();
                CustomQuery.GetData().Remove(product);
                prd.thumbnail = "https://source.unsplash.com/random/350x350?sig=" + prd.id;
                CustomQuery.GetData().Add(prd);

                return RedirectToAction("Index");
            }
            return View(prd);
        }

        // Delete product
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            var product = CustomQuery.GetData().Where(s => s.id == id).FirstOrDefault();
           CustomQuery.GetData().Remove(product);
            return RedirectToAction("Index");
        }
    }
}