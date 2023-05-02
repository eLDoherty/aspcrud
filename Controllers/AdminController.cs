using System;
using System.Collections.Generic;
using System.Web.Mvc;
using learnnet.Helper;
using learnnet.Models;
using Newtonsoft.Json;

namespace learnnet.Controllers
{
    public class AdminController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();
        readonly UserQuery UQ = new UserQuery();
        readonly Permission PM = new Permission();
        readonly Pagination PG = new Pagination();

        // GET: Admin
        [Authorize]
        public ActionResult UserList()
        {
            string test = Response.Cookies.ToString();
            bool isSuperadmin = CustomQuery.IsSuperAdmin(User.Identity.Name);
            var data = Pagination.PaginatePerPageUser(2);
            return View(data);
        }

        // Order By ID
        public string OrderUserById()
        {
            return "";
        }

        // Handle Request Ajax Per Page 
        public string PaginationPerPage(int totalDisplay)
        {
            var data = Pagination.PaginatePerPageUser(totalDisplay);

            return JsonConvert.SerializeObject(data);
        }

        public string PaginationPerStep(int page, int rows, string sorting, string name)
        {

            var data = Pagination.PaginatePerStep(page, rows, sorting, name);

            return JsonConvert.SerializeObject(data);
        }

        public string PaginationById(string sorting, int rows)
        {
            var data = Pagination.PaginateByUserId(sorting, rows);

            return JsonConvert.SerializeObject(data);
        }

        public string PaginationByUsername(string sorting, int rows)
        {
            var data = Pagination.PaginateByUsername(sorting, rows);

            return JsonConvert.SerializeObject(data);
        }

        public string PaginationByEmail(string sorting, int rows)
        {
            var data = Pagination.PaginateByUserEmail(sorting, rows);

            return JsonConvert.SerializeObject(data);
        }

        public ActionResult DeleteUser(int id) 
        {
            if (CustomQuery.DeleteUserById(id)) 
            {
                TempData["message"] = "User has been deleted!";
                return RedirectToAction("UserList", "Admin");
            }
            TempData["message"] = "Failed delete user!";
            return RedirectToAction("UserList", "Admin");

        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            if (AdminAuthorization())
            {
                Previlege prevs = CustomQuery.UserPrevilege(id);
                List<Section> sections = CustomQuery.AllSections();
                User user = CustomQuery.SelectUser(id);
                ViewBag.Previleges = prevs;
                ViewBag.Sections = sections;
                return View(user);
            }
            else
            {
                return UnauthorizedRedirection();
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(User user, string[] pageId, string[] addition, string[] edition, string[] deletion, string[] role)
        {
            var page = pageId;
            // Collection from edit user form
            int id = user.id;

            var deleteOldAccessbility = CustomQuery.DeleteAccessbility(user.id);

            for(int i = 0; i < page.Length; i++)
            {
                int sectionId = Convert.ToInt32(page[i]);
                int add = addition[i] == "true" && addition[i] != null ? 1 : 0;
                int edit = edition[i] == "true" && edition[i] != null ? 1 : 0;
                int delete = deletion[i] == "true" && deletion[i] != null ? 1 : 0;
                CustomQuery.AddAccessbility(sectionId, add, edit, delete, user.id, role[i]);
            }

            // Edit user 
            var changeUser = UserQuery.EditUser(user);

            if (changeUser)
            {
                TempData["message"] = "User has been updated";
                return RedirectToAction("UserList", "Admin");
            }
            TempData["message"] = "Failed edit user";
            return View(user);   
        }

        
        public ActionResult AddUser()
        {
            bool isSuperadmin = CustomQuery.IsSuperAdmin(User.Identity.Name);

            if (AdminAuthorization() && isSuperadmin)
            {
                return View();
            }
           else
            {
                return UnauthorizedRedirection();
            }
        }

        // Add new user from dashboard
        [System.Web.Mvc.HttpPost]
        public ActionResult AddUser(User user)
        {
            var createUser = UserQuery.CreateUser(user);

            if (createUser)
            {
                TempData["message"] = "New user has been created!";
                return RedirectToAction("UserList", "admin");
            }

            TempData["message"] = "New user has been created!";
            return RedirectToAction("AddUser", "admin");
        }

        // Admin Authorization
        protected bool AdminAuthorization()
        {
            if (!CustomQuery.IsAdmin(User.Identity.Name))
            {
                return false;
            }
            return true;
        }

        protected ActionResult UnauthorizedRedirection()
        {
            TempData["authorization"] = "Login as superadmin / authorize admin to access the page";
            return RedirectToAction("Login", "Auth");
        }
    }
}
