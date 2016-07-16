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
using System.IO;

namespace WorkWarriors.Controllers
{
    public class HomeownerComfirmedBidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HomeownerComfirmedBids
        public ActionResult Index()
        {
            return View(db.HomeownerComfirmedBids.ToList());
        }

        // GET: HomeownerComfirmedBids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
            if (homeownerComfirmedBids == null)
            {
                return HttpNotFound();
            }
            return View(homeownerComfirmedBids);
        }

        // GET: HomeownerComfirmedBids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeownerComfirmedBids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice")] HomeownerComfirmedBids homeownerComfirmedBids)
        {
            if (ModelState.IsValid)
            {
                db.HomeownerComfirmedBids.Add(homeownerComfirmedBids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeownerComfirmedBids);
        }

        // GET: HomeownerComfirmedBids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
            HomeownerComfirmedBids homeownerPics = db.HomeownerComfirmedBids.Include(i => i.ServiceRequestPaths).SingleOrDefault(i => i.ID == id);
            if (homeownerComfirmedBids == null)
            {
                return HttpNotFound();
            }
            return View(homeownerComfirmedBids);
        }

        // POST: HomeownerComfirmedBids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice,Service_Number")] HomeownerComfirmedBids homeownerComfirmedBids, IEnumerable<HttpPostedFileBase> files)
        {
            homeownerComfirmedBids.AfterPaths = new List<AfterPath>();
            if (ModelState.IsValid)
            {
                if (files == null)
                {
                    db.Entry(homeownerComfirmedBids).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                foreach (var file in files)
                {

                    if (file != null && file.ContentLength > 0)
                    {


                        var photo = new AfterPath() { FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName) };
                        file.SaveAs(Path.Combine(Server.MapPath("~/images"), photo.FileName));
                        photo.HomeownerComfirmedBidsID = homeownerComfirmedBids.ID;
                        db.AfterPaths.Add(photo);




                    }


                }
                db.Entry(homeownerComfirmedBids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Confirm", homeownerComfirmedBids);
            }

            return View(homeownerComfirmedBids);
        }

        // GET: HomeownerComfirmedBids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
            if (homeownerComfirmedBids == null)
            {
                return HttpNotFound();
            }
            return View(homeownerComfirmedBids);
        }

        // POST: HomeownerComfirmedBids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
            db.HomeownerComfirmedBids.Remove(homeownerComfirmedBids);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Confirm(int? id)
        {

            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            var confirmedList = db.HomeownerComfirmedBids.ToList();
            string ConEmail1 = "";
            string ConEmail2 = "";
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();
            HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);

            if (homeownerComfirmedBids.Completed == true)
            {
                return RedirectToAction("Already_Confirmed_Completion", "CompletedBids");
            }

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    ConEmail2 = user.Email;
                }
            }

            foreach (var i in confirmedList)
            {
                if (id == i.ID)
                {
                    ConEmail1 = i.ConEmail;
                }
            }

            if (this.User.IsInRole("Admin") || ConEmail1 == ConEmail2)
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
            if (homeownerComfirmedBids == null)
            {
                return HttpNotFound();
            }
            return View(homeownerComfirmedBids);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }

        public ActionResult Already_Confirmed()
        {
            ViewBag.Message = "You have already confirmed a contractor for this service request.";

            return View();
        }

        public ActionResult After(int? id)
        {

            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            
            var confirmedList = db.HomeownerComfirmedBids.ToList();
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

            foreach (var i in confirmedList)
            {
                if (id == i.ID)
                {
                    ConEmail1 = i.ConEmail;
                }
            }

            if (this.User.IsInRole("Admin") || ConEmail1 == ConEmail2)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                HomeownerComfirmedBids homeownerComfirmedBids = db.HomeownerComfirmedBids.Find(id);
                if (homeownerComfirmedBids == null)
                {
                    return HttpNotFound();
                }
                return View(homeownerComfirmedBids);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
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
