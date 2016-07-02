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
    public class HomeownersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Homeowners
        public ActionResult Index(string clientUsername, string searchString)
        {
            var UsernameLst = new List<string>();

            var UsernameQry = from d in db.Homeowners
                              orderby d.Username
                              select d.Username;

            UsernameLst.AddRange(UsernameQry.Distinct());
            ViewBag.clientUsername = new SelectList(UsernameLst);

            var homeowners = from m in db.Homeowners
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                homeowners = homeowners.Where(s => s.LastName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(clientUsername))
            {
                homeowners = homeowners.Where(x => x.Username == clientUsername);
            }

            return View(homeowners);
        }

        // GET: Homeowners/Details/5
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

        // GET: Homeowners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Homeowners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email")] Homeowner homeowner)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Must_be_logged_in", "Homeowners");
            }
            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    homeowner.email = user.Email;
                }
            }
            if (ModelState.IsValid)
            {
                db.Homeowners.Add(homeowner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeowner);
        }

        // GET: Homeowners/Edit/5
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

        // POST: Homeowners/Edit/5
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

        // GET: Homeowners/Delete/5
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

        // POST: Homeowners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homeowner homeowner = db.Homeowners.Find(id);
            db.Homeowners.Remove(homeowner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Must_be_logged_in()
        {
            ViewBag.Message = "You must log in as a registered user to create a homeowner profile";

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
