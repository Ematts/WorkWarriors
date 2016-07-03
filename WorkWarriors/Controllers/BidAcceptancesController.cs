//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using WorkWarriors.Models;

//namespace WorkWarriors.Controllers
//{
//    public class BidAcceptancesController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: BidAcceptances
//        public ActionResult Index()
//        {
//            return View(db.BidAcceptances.ToList());
//        }

//        // GET: BidAcceptances/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            BidAcceptance bidAcceptance = db.BidAcceptances.Find(id);
//            if (bidAcceptance == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bidAcceptance);
//        }

//        // GET: BidAcceptances/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: BidAcceptances/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,Address,City,State,Zip,PostedDate,Bid,CompletionDeadline,Description")] BidAcceptance bidAcceptance)
//        {
//            if (ModelState.IsValid)
//            {
//                db.BidAcceptances.Add(bidAcceptance);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(bidAcceptance);
//        }

//        // GET: BidAcceptances/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            BidAcceptance bidAcceptance = db.BidAcceptances.Find(id);
//            if (bidAcceptance == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bidAcceptance);
//        }

//        // POST: BidAcceptances/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,Address,City,State,Zip,PostedDate,Bid,CompletionDeadline,Description")] BidAcceptance bidAcceptance)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(bidAcceptance).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(bidAcceptance);
//        }

//        // GET: BidAcceptances/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            BidAcceptance bidAcceptance = db.BidAcceptances.Find(id);
//            if (bidAcceptance == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bidAcceptance);
//        }

//        // POST: BidAcceptances/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            BidAcceptance bidAcceptance = db.BidAcceptances.Find(id);
//            db.BidAcceptances.Remove(bidAcceptance);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
