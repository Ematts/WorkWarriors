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
        public ActionResult Calculate(int? ID, int? id)

        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }

            var conList = db.HomeownerComfirmedBids.ToList();
            string ConEmail1 = "";
            string ConEmail2 = "";
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    ConEmail2 = user.Email;
                }
            }

            foreach (var i in conList)
            {
                if (id == i.ID)
                {
                    ConEmail1 = i.ConEmail;
                }
            }

            if (this.User.IsInRole("Admin") || ConEmail1 == ConEmail2)
            {


                HomeownerComfirmedBids bid = new HomeownerComfirmedBids();
                bid = db.HomeownerComfirmedBids.Find(ID);
                return View(bid);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
       }
        

        public ActionResult RouteSubmission()

        {
            return View();
        }
    }
}