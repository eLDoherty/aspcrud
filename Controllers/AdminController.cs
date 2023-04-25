﻿using System.Web.Mvc;
using learnnet.Helper;
using learnnet.Models;

namespace learnnet.Controllers
{
    public class AdminController : Controller
    {
        readonly CustomQuery CQ = new CustomQuery();
        readonly UserQuery UQ = new UserQuery();

        // GET: Admin
        public ActionResult UserList()
        {
            string test = Response.Cookies.ToString();
            bool isSuperadmin = CustomQuery.IsSuperAdmin(User.Identity.Name);
            if (AdminAuthorization() && isSuperadmin)
            {
                var data = CustomQuery.GetAllUser();
                return View(data);
            }

            return UnauthorizedRedirection();

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
                User user = CustomQuery.SelectUser(id);
                ViewBag.Previleges = prevs;
                return View(user);
            }
            else
            {
                return UnauthorizedRedirection();
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(User user, FormCollection collection)
        {

            // Collection from edit user form
            var data = user;
            var test = collection.GetValue("mediaPage");
            var test1 = data;
            bool create = collection.GetValue("canCreate") != null ? true : false;
            bool edit = collection.GetValue("canEdit") != null ? true : false;
            bool delete = collection.GetValue("canDelete") != null ? true : false;
            bool productAccess = collection.GetValue("productPage") != null ? true : false;
            bool categoryAccess = collection.GetValue("categoryPage") != null ? true : false;
            bool mediaAccess = collection.GetValue("mediaPage") != null ? true : false;
            bool draftAccess = collection.GetValue("draftPage") != null ? true : false;

            // Edit user 
            var changeUser = UserQuery.EditUser(user);

            // Delete Previous Previlege
            var deleteOldPrevilege = CustomQuery.DeletePrevilege(user.id);

            // Add newest previlege
            var setPrevilege = UserQuery.SetPrevilegeUser(user.id, create, edit, delete);

            // Delete old accesbility
            var deleteOldAccess = CustomQuery.DeleteAccesbility(user.id);

            // Add newest accesbility
            var setUserAccesbility = UserQuery.SetUserAccesbility(user.id, productAccess, categoryAccess, mediaAccess, draftAccess);

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
