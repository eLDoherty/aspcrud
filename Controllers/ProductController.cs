using System.Linq;
using System.Web.Mvc;
using learnnet.Models;
using System.Data;
using learnnet.Helper;
using System.Web;
using System.IO;
using System;
using System.Web.Script.Serialization;

namespace learnnet.Controllers
{
    public class ProductController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();

        public ActionResult Index()
        {
            ViewBag.Description = "Product page with CRUD";
            var data = CustomQuery.GetDataPagination(1);
            return View(data);
        }

        public ActionResult Draft()
        {
            ViewBag.Description = "Your draft list, ready to publish";
            var data = CustomQuery.GetDataPagination(1);
            return View(data);
        }

        [System.Web.Mvc.HttpPost]
        public string Pagination(int page)
        {
            if(page > 0 || page != 0)
            {
                var data = CustomQuery.GetDataPagination(page);
                return new JavaScriptSerializer().Serialize(data);
            } else
            {
                return "Nothing";
            }
        }

        //Create Product Handler
        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Product prd, HttpPostedFileBase thumbnail, FormCollection form)
        {
            var data = prd;
            var data1 = form;

            if (ModelState.IsValid)
            {
                var product = CustomQuery.GetData()?.Where(s => s.name == prd.name).FirstOrDefault();

                if (product != null)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (thumbnail != null)
                {
                    string productThumbnailName = "IMG-" + DateTime.Now.Ticks.ToString() + "-" + thumbnail.FileName;
                    string fileName =  Path.GetFileName(productThumbnailName);
                    thumbnail.SaveAs(path + fileName);
                    var pushData = CustomQuery.InsertData(prd, productThumbnailName);
                    string cats = form["category"];
                    var pushCategory = CustomQuery.InsertCategory(form["category"]);

                    if (pushData)
                    {
                        TempData["message"] = "Product addition successfully!";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(prd);
        }
         
        // Edit product
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            return View(CustomQuery.ChooseData(id));
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Product prd, HttpPostedFileBase thumbnail)
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
                var currentProduct = CustomQuery.GetData().Where(s => s.id == prd.id).FirstOrDefault();
                string productThumbnailName = currentProduct.thumbnail;

                if (thumbnail != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    productThumbnailName = "IMG-" + DateTime.Now.Ticks.ToString() + "-" + thumbnail.FileName;
                    string fileName = Path.GetFileName(productThumbnailName);
                    thumbnail.SaveAs(path + fileName);
                } 
                var update = CustomQuery.EditData(prd, productThumbnailName);
                TempData["message"] = "Edit data successfully!";
                return RedirectToAction("Index");
            }
            return View(prd);
        }

        // Delete product
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            var removeData = CustomQuery.DeletData(id);
            if (removeData)
            {
                TempData["message"] = "Data has been removed!";
                return RedirectToAction("Index");
            } else
            {
                TempData["message"] = "Delete data failed!";
                return RedirectToAction("Index");
            }
        }
    }
}