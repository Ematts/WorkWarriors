﻿using System;
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

        public ActionResult HomeownerConfirmation(int? id)
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
