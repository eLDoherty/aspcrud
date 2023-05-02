using System;
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
        public ActionResult Login(string ReturnUrl)
        {
            if (CustomQuery.LoggedInUser(User.Identity.Name)) {
                TempData["logged"] = "You are already logged in";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Request login
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
           
            if (UserQuery.IsValidUser(user.email, user.password))
            {
                string userData = user.email;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,                                     // ticket version
                    user.email,                            // authenticated username
                    DateTime.Now,                          // issueDate
                    DateTime.Now.AddHours(1),              // expiryDate
                    false,                                 // true to persist across browser sessions
                    userData,                              // can be used to store additional user data
                    FormsAuthentication.FormsCookiePath ); // the path for the cookie

                // Encrypt the ticket using the machine key
                // ticket.Expiration = DateTime.Now.AddSeconds(60);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Add the cookie to the request to save it
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                // Your redirect logic
                TempData["autoLogout"] = "You're session has been expired, please login again";
                Response.Redirect(FormsAuthentication.GetRedirectUrl(user.email, false));
            
            }
            else
            {
                ModelState.AddModelError("email", "Your Email or Password is incorrect");
            }
            return View(user);
        }

        // GET: Auth/Details
        [System.Web.Mvc.HttpGet]
        public ActionResult Register()
        {
            if (CustomQuery.LoggedInUser(User.Identity.Name))
            {
                TempData["logged"] = "You are already logged in";
                return RedirectToAction("Index", "Home");
            }
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
