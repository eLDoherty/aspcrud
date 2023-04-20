using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using learnnet.Models;
using learnnet.Helper;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;

namespace learnnet.Controllers
{
    public class AuthController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString);
        readonly UserQuery UQ = new UserQuery();
        readonly CustomQuery CQ = new CustomQuery();

        [System.Web.Mvc.HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Request logon
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (UserQuery.IsValidUser(user.email, user.password))
            {
                FormsAuthentication.SetAuthCookie(user.email, false);
                TempData["loginSuccess"] = "You're logged in";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("email", "Your Email or Password is incorrect");
            }
            return View(user);
        }


        // GET: Auth/Details/5
        [System.Web.Mvc.HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Create User
        [System.Web.Mvc.HttpPost]
        public ActionResult Register(User user)
        {
            var data = user;
            try
            {
                if (ModelState.IsValid)
                {
                    bool isExist = UserQuery.IsUserExist(user.email);
                    //checking if user already exist
                    if (!isExist)
                    {
                        string query = "INSERT INTO dbo.users (username, email, role, password) VALUES (@username,@email, 'user', @password)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@username", user.username);
                            cmd.Parameters.AddWithValue("@email", user.email);
                            // encode password i'm using simple base64 you can use any more secure algo
                            cmd.Parameters.AddWithValue("@password", UserQuery.Base64Encode(user.password));
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            con.Close();
                            if (i > 0)
                            {
                                FormsAuthentication.SetAuthCookie(user.email, false);
                                TempData["registerSuccess"] = "Register success!";
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("email", "something went wrong try later!");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("email", "User with same email already exist!");
                    }

                }
                else
                {
                    ModelState.AddModelError("email", "Data is not correct");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.Message);
            }
            return View(user);
        }

        // POST: Auth/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auth/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auth/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
