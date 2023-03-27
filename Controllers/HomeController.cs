using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using learnnet.Models;

namespace learnnet.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Description = "Learn .NET MVC -- Front Page";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. Testing";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       public ActionResult Test()
        {
            ViewBag.Message = "This is my first asp page. My Test";

            return View();
        }
    }
}