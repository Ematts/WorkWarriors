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
    public class CompletedBidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompletedBids
        public ActionResult Index()
        {
            return View(db.CompletedBids.ToList());
        }

        // GET: CompletedBids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompletedBids completedBids = db.CompletedBids.Find(id);
            if (completedBids == null)
            {
                return HttpNotFound();
            }
            return View(completedBids);
        }

        // GET: CompletedBids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompletedBids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice")] CompletedBids completedBids)
        {
            if (ModelState.IsValid)
            {
                db.CompletedBids.Add(completedBids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(completedBids);
        }

        // GET: CompletedBids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompletedBids completedBids = db.CompletedBids.Find(id);
            if (completedBids == null)
            {
                return HttpNotFound();
            }
            return View(completedBids);
        }

        // POST: CompletedBids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice")] CompletedBids completedBids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(completedBids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(completedBids);
        }

        // GET: CompletedBids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompletedBids completedBids = db.CompletedBids.Find(id);
            if (completedBids == null)
            {
                return HttpNotFound();
            }
            return View(completedBids);
        }

        // POST: CompletedBids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompletedBids completedBids = db.CompletedBids.Find(id);
            db.CompletedBids.Remove(completedBids);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CompletedBids(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompletedBids completedBids = db.CompletedBids.Find(id);
            if (completedBids == null)
            {
                return HttpNotFound();
            }
            return View(completedBids);
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
