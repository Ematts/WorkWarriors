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

namespace WorkWarriors.Controllers
{
    public class ContractorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contractors
        public ActionResult Index(string clientUsername, string searchString)
        {
            var UsernameLst = new List<string>();

            var UsernameQry = from d in db.Contractors
                              orderby d.Username
                              select d.Username;

            UsernameLst.AddRange(UsernameQry.Distinct());
            ViewBag.clientUsername = new SelectList(UsernameLst);

            var contractors = from m in db.Contractors
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                contractors = contractors.Where(s => s.LastName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(clientUsername))
            {
                contractors = contractors.Where(x => x.Username == clientUsername);
            }

            return View(contractors);
        }

        // GET: Contractors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // GET: Contractors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email")] Contractor contractor)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Must_be_logged_in", "Contractors");
            }

            if (!this.User.IsInRole("Admin") && (!this.User.IsInRole("Contractor")))
            {
                return RedirectToAction("Must_be_logged_in", "Contractors");
            }

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    contractor.email = user.Email;
                    contractor.Username = user.Screen_Name;
                }
            }
            if (ModelState.IsValid)
            {
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Must_be_logged_in()
        {
            ViewBag.Message = "You must log in as a registered user to create a contractor profile";

            return View();
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
