using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;

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
        public ActionResult Edit([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice")] HomeownerComfirmedBids homeownerComfirmedBids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeownerComfirmedBids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
