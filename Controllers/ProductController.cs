using System.Linq;
using System.Web.Mvc;
using learnnet.Models;
using System.Data;
using learnnet.Helper;

namespace learnnet.Controllers
{
    public class ProductController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();

        public ActionResult Index()
        {
            ViewBag.Description = "Product page with CRUD";
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

                if (product != null)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                var pushData =  CustomQuery.InsertData(prd.name, prd.price);
                if (pushData)
                {
                    TempData["message"] = "Product addition successfully!";
                    return RedirectToAction("Index");
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

                var update = CustomQuery.EditData(prd);
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