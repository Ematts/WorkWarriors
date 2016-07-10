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
    public class CompletedBidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompletedBids
        public ActionResult Index()
        {
             if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            return View(db.CompletedBids.ToList());
        }

        // GET: CompletedBids/Details/5
        public ActionResult Details(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
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
        public ActionResult Create([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice,ConstactorPaid,ConstactorDue")] CompletedBids completedBids, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var picture = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Picture,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        picture.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    completedBids.Files = new List<File> { picture };
                }

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
        public ActionResult Edit([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice,ContractorPaid,ContractorDue")] CompletedBids completedBids)
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

        public ActionResult Payment(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            var completedList = db.CompletedBids.ToList();
            string HomeOwnerEmail = "";
            string payeeEmail = "";
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();
            

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    payeeEmail = user.Email;
                }
            }

            foreach (var i in completedList)
            {
                if(id == i.ID)
                {
                    HomeOwnerEmail = i.HomeEmail;
                }
            }


            if (this.User.IsInRole("Admin") || HomeOwnerEmail == payeeEmail)
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
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
       }

        public ActionResult AdminPaymentsDue()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }

            var dueList = db.CompletedBids.ToList();
            List<CompletedBids> payList = new List<CompletedBids>();

            foreach (var i in dueList)
            {
                if (i.ContractorPaid == false)
                {
                    payList.Add(i);
                }
            }

            return View(payList);
        }


        public ActionResult PayContractor(int? id)
        {

            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompletedBids completedBids = db.CompletedBids.Find(id);
            if (completedBids == null)
            {
                return HttpNotFound();
            }

            if (completedBids.ContractorPaid == true)
            {
                return RedirectToAction("Already_Paid", "CompletedBids");
            }

            return View(completedBids);
        }

        public ActionResult Already_Paid()
        {
            ViewBag.Message = "This contractor has already been paid for this job.";

            return View();
        }

        public ActionResult Already_Confirmed_Completion()
        {
            ViewBag.Message = "You have already confirmed completion for this job.";

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
