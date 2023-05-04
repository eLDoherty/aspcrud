using System.Linq;
using System.Web.Mvc;
using learnnet.Models;
using System.Data;
using learnnet.Helper;
using System.Web;
using System.IO;
using System;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace learnnet.Controllers
{
    public class ProductController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();
        readonly Permission permission = new Permission();
        readonly Paging PG = new Paging();

        [Authorize]
        public ActionResult Index()
        {   
            ViewBag.Description = "Product page with CRUD";
            ViewBag.Countries = CustomQuery.GetSelectedCountries();
            var data = CustomQuery.GetDataPagination(1);
            return View(data);
        }

        [Authorize]
        public ActionResult Draft()
        {
            if (Permission.CanEditProduct(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                ViewBag.Description = "Your draft list, ready to publish";
                var data = CustomQuery.GetData();
                return View(data);
            }
            return UnauthorizedRedirection();
        }

        // Pagination request from ajax
        [System.Web.Mvc.HttpPost]
        public string Pagination(int page)
        {
            if(page > 0 || page != 0)
            {
                var data = CustomQuery.GetDataPagination(page);
                var test = data;
                return new JavaScriptSerializer().Serialize(data); 
            } else
            {
                return "Nothing";
            }
        }

        // Filter request from ajax
        [System.Web.Mvc.HttpPost]
        public object FilterProduct(string country)
        {
            var data = CustomQuery.FilterProductData(country);
            return new JavaScriptSerializer().Serialize(data);
        }

        //Create Product Handler
        [Authorize]
        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
 
            if (Permission.CanAddProduct(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                ViewBag.Categories = CustomQuery.GetCategories();
                ViewBag.Countries = CustomQuery.getCountries();
                ViewBag.Images = CustomQuery.GetImageMedia();

                return View();
            }

            return UnauthorizedRedirection();

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Product prd, string[] productCategory)
        {
            var thumbnail = prd.thumbnail;
            var test = thumbnail;
            if (ModelState.IsValid)
            {
                var product = CustomQuery.GetData()?.Where(s => s.name == prd.name).FirstOrDefault();

                if (product != null)
                {
                    ModelState.AddModelError("Name", "The product is already exist");
                    return View(prd);
                }

                int pushData = CustomQuery.InsertData(prd);
                var pushProductCategory = CustomQuery.ProductCategory(productCategory, pushData);
                if (pushProductCategory)
                {
                    TempData["message"] = "Product addition successfully!";
                    return RedirectToAction("Index");
                }
            }
            TempData["message"] = "Product addition failed!";
            ViewBag.Categories = CustomQuery.GetCategories();
            ViewBag.Countries = CustomQuery.getCountries();
            ViewBag.Images = CustomQuery.GetImageMedia();
            return View(prd);
        }

        // Edit product
        [Authorize()]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {

            if (Permission.CanEditProduct(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                ViewBag.TableCategories = CustomQuery.GetCategories();
                ViewBag.Categories = CustomQuery.GetCategoriesById(id);
                ViewBag.Countries = CustomQuery.getCountries();
                ViewBag.Images = CustomQuery.GetImageMedia();
                return View(CustomQuery.ChooseData(id));
            }

            return UnauthorizedRedirection();
          
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Product prd, string[] productCategory)
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

                var deletePrevCategories = CustomQuery.DeletePrevCategories(prd.id);
                var pushNewCategory = CustomQuery.ProductCategory(productCategory, prd.id);
                var update = CustomQuery.EditData(prd);

                if( update == true)
                {
                    TempData["message"] = "Edit data successfully!";

                    return RedirectToAction("Index");
                }
            }
            TempData["message"] = "Edit data failed!";
            return View(prd);
        }

        // Delete product
        [Authorize]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            if (Permission.CanDeleteProduct(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                var removeData = CustomQuery.DeletData(id);
                if (removeData)
                {
                    TempData["message"] = "Data has been removed!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Delete data failed!";
                    return RedirectToAction("Index");
                }
            }

            return UnauthorizedRedirection();
        }

        // Product Category 
        [Authorize]
        [System.Web.Mvc.HttpGet]
        public ActionResult Category()
        {
            if (Permission.CanAddCategory(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                ViewBag.Category = Paging.PaginatePerPageCategory(0);
                return View();
            }
            return UnauthorizedRedirection();
        }

        // Product Category 
        [System.Web.Mvc.HttpPost]
        public ActionResult Category(Category cat)
        {
            if (ModelState.IsValid)
            {
                var insertCatgeory = CustomQuery.InsertCategory(cat);
                if (insertCatgeory)
                {
                    TempData["message"] = "Category has been added!";
                    ViewBag.Category = Paging.PaginatePerPageCategory(0);
                    return View();
                }
            }
            TempData["message"] = "Category not added!";
            return View(cat);
        }

        // Edit Category 
        [System.Web.Mvc.HttpPost]
        public ActionResult EditCategory(Category cat)
        {
            var data = cat;
            if (ModelState.IsValid)
            {
                var insertCatgeory = CustomQuery.EditCategory(cat);
                if (insertCatgeory)
                {
                    ViewBag.Category = Paging.PaginatePerPageCategory(0);
                    TempData["message"] = "Category has been updated";
                    return RedirectToAction("Category", "Product");
                }
            }
            TempData["message"] = "Cant edit the category!";
            return RedirectToAction("Category", "Product");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var deleteCategory = CustomQuery.DeleteCategoryById(id);
            if(deleteCategory)
            {
                TempData["message"] = "Category has been deleted";
                return RedirectToAction("Category", "Product");
            }
            TempData["message"] = "Category cant be deleted";
            return RedirectToAction("Category", "Product");
        }

        // Handle Request Ajax Per Page - Category
        public string PaginationPerPage(int totalDisplay)
        {
            var data = Paging.PaginatePerPageCategory(totalDisplay);

            return JsonConvert.SerializeObject(data);
        }

        // Ajax Per Step = Category
        public string PaginationPerStep(int page, int rows, string sorting, string name)
        {
            var data = Paging.PaginatePerStepCategory(page, rows, sorting, name);

            return JsonConvert.SerializeObject(data);
        }

        // Sorting ajax by user id - Category
        public string PaginationCategoryById(string sorting, int rows, int page)
        {
            var data = Paging.ShowCategoryByID(sorting, rows, page);

            return JsonConvert.SerializeObject(data);
        }

        // Sorting ajax by user id - Category
        public string PaginationCategory(string sorting, int rows, int page)
        {
            var data = Paging.PaginateByCategory(sorting, rows, page);

            return JsonConvert.SerializeObject(data);
        }

        // Search category
        public string SearchCategory(string key)
        {
            var data = Paging.SearchCategoryAjax(key);
            return JsonConvert.SerializeObject(data);
        }

        // Media page
        [Authorize]
        public ActionResult Media()
        {
            if (Permission.CanAddMedia(CustomQuery.GetCurrentUserId(User.Identity.Name)))
            {
                var data = CustomQuery.GetImageMedia();
                ViewBag.Images = data;
                return View();
            }

            return UnauthorizedRedirection();
            
        }

        // Media
        [System.Web.Mvc.HttpPost]
        public ActionResult Media(HttpPostedFileBase uri, string name) 
        {
            var data = CustomQuery.GetImageMedia();
            ViewBag.Images = data;
            if (uri != null)
            {
                string path = Server.MapPath("~/Uploads/");
                string mediaImageName = "IMG-" + DateTime.Now.Ticks.ToString() + "-" + uri.FileName;
                string fileName = Path.GetFileName(mediaImageName);
                uri.SaveAs(path + fileName);
                bool insertMediaImage = CustomQuery.AddMediaImage(mediaImageName, name);
                if (insertMediaImage)
                {
                    TempData["message"] = "Your image has been added";
                    ViewBag.Images = data;
                    return RedirectToAction("Media");
                }
            }
            TempData["message"] = "Failed to upload!";
            ViewBag.Images = data;
            return View();
        }

        protected ActionResult UnauthorizedRedirection()
        {
            TempData["authorization"] = "Login as superadmin / authorized admin to access the page";
            return RedirectToAction("Index", "Home");
        }
    }
}