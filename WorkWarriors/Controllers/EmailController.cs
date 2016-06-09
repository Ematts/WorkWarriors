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



namespace WorkWarriors.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Email
        public ActionResult Index()
        {
            return View(db.Homeowners.ToList());
        }

        // GET: Email/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homeowner homeowner = db.Homeowners.Find(id);
            if (homeowner == null)
            {
                return HttpNotFound();
            }
            return View(homeowner);
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Email/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email")] Homeowner homeowner)
        {
            if (ModelState.IsValid)
            {
                db.Homeowners.Add(homeowner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeowner);
        }

        // GET: Email/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homeowner homeowner = db.Homeowners.Find(id);
            if (homeowner == null)
            {
                return HttpNotFound();
            }
            return View(homeowner);
        }

        // POST: Email/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email")] Homeowner homeowner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeowner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homeowner);
        }

        // GET: Email/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homeowner homeowner = db.Homeowners.Find(id);
            if (homeowner == null)
            {
                return HttpNotFound();
            }
            return View(homeowner);
        }

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homeowner homeowner = db.Homeowners.Find(id);
            db.Homeowners.Remove(homeowner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult sendMailNow()///(string message)
        {

            db = new ApplicationDbContext();
            var myMessage = new SendGrid.SendGridMessage();

            List<String> recipients = new List<String> { };
            //{
                //@"Changa Chimp <changachimp@yahoo.com>",
                //@"Penny Wise <wisepenny79@gmail.com>",
                //@"E. Matts <erickmattson@msn.com>",
            //};
            
            foreach (var i in db.Homeowners)
                {

                recipients.Add(i.email);

                };

            myMessage.AddTo(recipients);
            myMessage.From = new MailAddress("monsymonster@msn.com", "Joe Johnson");
            myMessage.Subject = "Sending with SendGrid is Fun";
            //myMessage.Html = "<p>Hello World!</p>";
            myMessage.Text = "Welcome to Work Warriors";
            var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);

            return RedirectToAction("About", "Home");

        }

        public ActionResult sendContractorMail()
        {

            db = new ApplicationDbContext();
            var myMessage = new SendGrid.SendGridMessage();

            List<String> recipients = new List<String> { };

            foreach (var i in db.Contractors)
            {

                recipients.Add(i.email);

            };

            myMessage.AddTo(recipients);
            myMessage.From = new MailAddress("monsymonster@msn.com", "Joe Johnson");
            myMessage.Subject = "New Service Request Posting!!";
            myMessage.Text = "Service request:";
            var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);

            return RedirectToAction("About", "Home");

        }

        public ActionResult postServiceRequest()
        {
            
            var myMessage = new SendGrid.SendGridMessage();
            var contractors = db.Contractors.ToList();
            var servicerequests = db.ServiceRequests.ToList();

            List<String> recipients = new List<String> { };

            foreach (var i in contractors)
            {

                recipients.Add(i.email);

            };

            foreach (var i in servicerequests)
            {
                if(i.posted == false)
                {
                    myMessage.AddTo(recipients);
                    myMessage.From = new MailAddress("monsymonster@msn.com", "Joe Johnson");
                    myMessage.Subject = "New Service Request Posting!!";
                    string url = "http://localhost:14703/ServiceRequests/ContractorAcceptance/" + i.ID;
                    string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href ="+url+"> Click Here </a>" ;
                    myMessage.Html = message;
                    var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    
                }
                i.posted = true;
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
