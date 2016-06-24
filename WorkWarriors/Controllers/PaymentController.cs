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
    public class PaymentController : Controller
    {
        // GET: Payment

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getPayment(int? ID)

        {
            CompletedBids bid = new CompletedBids();
            bid = db.CompletedBids.Find(ID);
            return View(bid);
        }

    }
}