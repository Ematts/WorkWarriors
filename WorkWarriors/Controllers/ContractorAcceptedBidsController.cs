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
    public class ContractorAcceptedBidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractorAcceptedBids
        public ActionResult Index()
        {
            return View(db.ContractorAcceptedBids.ToList());
        }

        // GET: ContractorAcceptedBids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorAcceptedBids contractorAcceptedBids = db.ContractorAcceptedBids.Find(id);
            if (contractorAcceptedBids == null)
            {
                return HttpNotFound();
            }
            return View(contractorAcceptedBids);
        }

        // GET: ContractorAcceptedBids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractorAcceptedBids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Confirmed")] ContractorAcceptedBids contractorAcceptedBids)
        {
            if (ModelState.IsValid)
            {
                db.ContractorAcceptedBids.Add(contractorAcceptedBids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractorAcceptedBids);
        }

        // GET: ContractorAcceptedBids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorAcceptedBids contractorAcceptedBids = db.ContractorAcceptedBids.Find(id);
            if (contractorAcceptedBids == null)
            {
                return HttpNotFound();
            }
            return View(contractorAcceptedBids);
        }

        // POST: ContractorAcceptedBids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ConUsername")]ContractorAcceptedBids contractorAcceptedBids)

        //,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,Conemail,Homeemail,PostedDate,Bid,CompletionDeadline,Description,confirmed")] ContractorAcceptedBids contractorAcceptedBids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractorAcceptedBids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractorAcceptedBids);
        }

        // GET: ContractorAcceptedBids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorAcceptedBids contractorAcceptedBids = db.ContractorAcceptedBids.Find(id);
            if (contractorAcceptedBids == null)
            {
                return HttpNotFound();
            }
            return View(contractorAcceptedBids);
        }

        // POST: ContractorAcceptedBids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractorAcceptedBids contractorAcceptedBids = db.ContractorAcceptedBids.Find(id);
            db.ContractorAcceptedBids.Remove(contractorAcceptedBids);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult acceptanceConfirmation(int id)
        {

            //if (this.User.IsInRole"conr"){

            //}
            string conEmail = "";
            string conName = "";
            var myMessage = new SendGrid.SendGridMessage();
            var contractors = db.Contractors.ToList();
            var servicerequests = db.ServiceRequests.ToList();
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var person = db.Contractors.Where(x => x.UserId == identity).SingleOrDefault();
            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    conEmail = user.Email;
                    //username1 = user.UserName;
                    foreach (var con in contractors)
                    {
                        if (con.email == conEmail)
                        {
                            conName = con.Username;
                        }
                    }
                }

            }



            foreach (var i in servicerequests)
            {

                if (id == i.ID)
                {
                    myMessage.AddTo(i.email);
                    myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                    myMessage.Subject = "New Service Request Posting!!";
                    string url = "http://localhost:14703/ServiceRequests/ContractorAcceptance/" + i.ID;
                    //string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href =" + url + "> Click Here </a>";
                    String message = "Hello " + i.FirstName + "," + "<br>" + "<br>" + conName + " has offered to accept your following service request:" + "<br>" + "<br>" + i.Description + "<br>" + "<br>" + "To confirm acceptance, click on link below: <br><a href =" + url + "> Click Here </a>";
                    myMessage.Html = message;
                    var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    Create();
                    db.SaveChanges();
                }
                //i.posted = true;
                db.SaveChanges();
            }

            return RedirectToAction("About", "Home");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
