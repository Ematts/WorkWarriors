using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using SendGrid;
using Microsoft.AspNet.Identity;

namespace WorkWarriors.Controllers
{
    public class MapsController : Controller
    {
        // GET: Maps
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Calculate(int? ID)

        {
            HomeownerComfirmedBids bid = new HomeownerComfirmedBids();
            bid = db.HomeownerComfirmedBids.Find(ID);
            return View(bid);
        }

        public ActionResult RouteSubmission()

        {
            return View();
        }
    }
}