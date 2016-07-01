using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkWarriors.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InvalidLogin()
        {
            ViewBag.Message = "Invalid Login Attempt.";

            return View();
        }
        public ActionResult Unauthorized_Access()
        {
            ViewBag.Message = "You are not authorized to view this page.";

            return View();
        }
        public ActionResult View_Databases()
        {
            ViewBag.Message = "Select Database";

            return View();
        }
    }
}