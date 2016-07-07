using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace WorkWarriors.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceRequests
        public ActionResult Index()
        {
            return View(db.ServiceRequests.ToList());
        }

        // GET: ServiceRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email,PostedDate,Bid,CompletionDeadline,Description,posted,Contractor")] ServiceRequest serviceRequest)
        {

            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Must_be_logged_in", "ServiceRequests");
            }

            if (!this.User.IsInRole("Admin") && (!this.User.IsInRole("Homeowner")))
            {
                return RedirectToAction("Must_be_logged_in", "ServiceRequests");
            }

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    serviceRequest.email = user.Email;
                    serviceRequest.Username = user.Screen_Name;
                    serviceRequest.FirstName = user.First_Name;
                    serviceRequest.LastName = user.Last_Name;
                    serviceRequest.PostedDate = DateTime.Now;
                }
            }

            if(serviceRequest.PostedDate > serviceRequest.CompletionDeadline)
            {
                return RedirectToAction("DateIssue", "ServiceRequests");
            }


            if (ModelState.IsValid)
            {
                db.ServiceRequests.Add(serviceRequest);
                db.SaveChanges();
                return RedirectToAction("Post", new { id = serviceRequest.ID });


            }

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email,PostedDate,Bid,CompletionDeadline,Description,posted,contractor")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(serviceRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ContractorAcceptance(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var serviceList = db.ServiceRequests.ToList();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }

            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();

            if (this.User.IsInRole("Admin") || this.User.IsInRole("Contractor"))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
                if (serviceRequest == null)
                {
                    return HttpNotFound();
                }

                if (serviceRequest.expired == true)
                {
                        return RedirectToAction("expired", "ServiceRequests");
                }

                return View(serviceRequest);
                }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }

        public ActionResult Post(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            var serviceList = db.ServiceRequests.ToList();
            string HomeOwnerEmail1 = "";
            string HomeOwnerEmail2 = "";
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();


            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    HomeOwnerEmail2 = user.Email;
                }
            }

            foreach (var i in serviceList)
            {
                if (id == i.ID)
                {
                    HomeOwnerEmail1 = i.email;
                }
            }

            if (this.User.IsInRole("Admin") || HomeOwnerEmail1 == HomeOwnerEmail2)
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
                if (serviceRequest == null)
                {
                    return HttpNotFound();
                }
                return View(serviceRequest);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }

        public ActionResult Must_be_logged_in()
        {
            ViewBag.Message = "You must log in as a registered homeowner to create a service request";

            return View();
        }

        public ActionResult DateIssue()
        {
            ViewBag.Message = "Completion deadline must be later than posted date";

            return View();
        }

        public ActionResult Expired()
        {
            ViewBag.Message = "This service request has expired because the completion deadline has passed.";

            return View();
        }



        //public ActionResult HomeownerConfirmation(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
        //    if (serviceRequest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(serviceRequest);
        //}
    }
}
