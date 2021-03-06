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
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List <string> conList = new List<string>();

        //private Contractor contractor = new Contractor();
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
        [System.Web.Mvc.HttpPost]
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
        [System.Web.Mvc.HttpPost]
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
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
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
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");

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
            var credentials = new NetworkCredential(name, pass);
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);

            return RedirectToAction("About", "Home");

        }

        public ActionResult sendContractorMail()
        {

            db = new ApplicationDbContext();
            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");

            List<String> recipients = new List<String> { };

            foreach (var i in db.Contractors)
            {

                recipients.Add(i.email);

            };

            myMessage.AddTo(recipients);
            myMessage.From = new MailAddress("monsymonster@msn.com", "Joe Johnson");
            myMessage.Subject = "New Service Request Posting!!";
            myMessage.Text = "Service request:";
            var credentials = new NetworkCredential(name, pass);
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);

            return RedirectToAction("About", "Home");

        }

        //public ActionResult postServiceRequest()
        //{

        //    var myMessage = new SendGrid.SendGridMessage();
        //    var contractors = db.Contractors.ToList();
        //    var servicerequests = db.ServiceRequests.ToList();
              //string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
              //string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
        //    List<String> recipients = new List<String> { };

        //    foreach (var i in contractors)
        //    {

        //        recipients.Add(i.email);

        //    };

        //    foreach (var i in servicerequests)
        //    {
        //        if(i.posted == false)
        //        {
        //            myMessage.AddTo(recipients);
        //            myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
        //            myMessage.Subject = "New Service Request Posting!!";
        //            string url = "http://localhost:14703/ServiceRequests/ContractorAcceptance/" + i.ID;
        //            string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href ="+url+"> Click Here </a>" ;
        //            myMessage.Html = message;
        //            var credentials = new NetworkCredential("name", "pass");
        //            var transportWeb = new SendGrid.Web(credentials);
        //            transportWeb.DeliverAsync(myMessage);

        //        }
        //        i.posted = true;
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("About", "Home");
        //}

        public ActionResult postServiceRequest(int id)
        {

            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            var contractors = db.Contractors.ToList();
            var homeowners = db.Homeowners.ToList();
            var servicerequests = db.ServiceRequests.ToList();
            List<String> recipients = new List<String> { };
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Post_must_be_logged", "Email");
            }

            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();
            foreach (var i in contractors)
            {

                recipients.Add(i.email);

            };

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    foreach (var i in servicerequests)
                    {
                        if (id == i.ID)
                        {
                            if (i.posted == false)
                            {
                                myMessage.AddTo(recipients);
                                myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                                myMessage.Subject = "New Service Request Posting!!";
                                string url = "http://localhost:14703/ServiceRequests/ContractorAcceptance/" + i.ID;
                                string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href =" + url + "> Click Here </a>";
                                myMessage.Html = message;
                                var credentials = new NetworkCredential(name, pass);
                                var transportWeb = new SendGrid.Web(credentials);
                                transportWeb.DeliverAsync(myMessage);
                                i.posted = true;
                                i.ServiceNumber = i.ID;
                                i.Confirmed = false;
                                
                            }

                            else
                            {
                                return RedirectToAction("DuplicatePost", "ServiceRequests");
                            }
                            
                        }
                    }
                }
                
            }
            db.SaveChanges();
            return RedirectToAction("About", "Home");
        }


        public ActionResult acceptanceConfirmation(int id)
        {

            //if (this.User.IsInRole"conr"){

            //}
            //}
            string ConEmail = "";
            string ConUserName = "";
            string ConFirstName = "";
            string ConLastName = "";
            string ConAddress = "";
            string ConCity = "";
            string ConState = "";
            string ConZip = "";
            int Invoice = 1;

            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            var contractors = db.Contractors.ToList();
            var servicerequests = db.ServiceRequests.ToList();
            var acceptList = db.ContractorAcceptedBids.ToList();
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var person = db.Contractors.Where(x => x.UserId == identity).SingleOrDefault();
            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    ConEmail = user.Email;
                    //username1 = user.UserName;
                    foreach (var con in contractors)
                    {
                        if (con.email == ConEmail)
                        {
                            ConUserName = con.Username;
                            ConFirstName = con.FirstName;
                            ConLastName = con.LastName;
                            ConAddress = con.Address;
                            ConCity = con.City;
                            ConState = con.State;
                            ConZip = con.Zip;
                        }
                    }
                }

            }



            foreach (var i in servicerequests)
            {

                if (id == i.ID)
                {

                    if(i.expired == true)
                    {
                        return RedirectToAction("expired", "ServiceRequests");
                    }

                    if(i.Confirmed == true)
                    {
                        return RedirectToAction("Already_Confirmed", "ContractorAcceptedBids");
                    }
                
                    ContractorAcceptedBids bid = new ContractorAcceptedBids();
                    bid.ServiceRequestPaths = new List<ServiceRequestPath>();
                    bid.ConUsername = ConUserName;
                    bid.HomeUsername = i.Username;
                    bid.ConFirstName = ConFirstName;
                    bid.HomeFirstname = i.FirstName;
                    bid.ConLastName = ConLastName;
                    bid.HomeLastName = i.LastName;
                    bid.ConAddress = ConAddress;
                    bid.HomeAddress = i.Address;
                    bid.ConCity = ConCity;
                    bid.HomeCity = i.City;
                    bid.ConState = ConState;
                    bid.HomeState = i.State;
                    bid.ConZip = ConZip;
                    bid.HomeZip = i.Zip;
                    bid.ConEmail = ConEmail;
                    bid.HomeEmail = i.email;
                    bid.PostedDate = i.PostedDate;
                    bid.CompletionDeadline = i.CompletionDeadline;
                    bid.Description = i.Description;
                    bid.Bid = i.Bid;
                    bid.ServiceNumber = i.ServiceNumber;
                    bid.Files = i.Files;
                    foreach (var x in acceptList)
                    {
                        if ((x.ConUsername == bid.ConUsername) && (x.ServiceNumber == bid.ServiceNumber))
                        {
                            return RedirectToAction("Duplicate", "ContractorAcceptedBids");
                        }
                    }
                    foreach (var x in i.ServiceRequestPaths)
                    {


                        bid.ServiceRequestPaths.Add(new ServiceRequestPath() { FileName = x.FileName });
               

                    }
                    bid.expired = i.expired;
                    bid.Confirmed = i.Confirmed;
                    //bid.ServiceNumber = i.ServiceNumber;
                    //foreach(var x in acceptList)
                    //{
                    //    if((x.ConUsername == bid.ConUsername) && (x.ServiceNumber == bid.ServiceNumber))
                    //        {
                    //        return RedirectToAction("Duplicate", "ContractorAcceptedBids");
                    //        }
                    //}
                    db.ContractorAcceptedBids.Add(bid);
                    db.SaveChanges();
                    Invoice = bid.ID;
                    myMessage.AddTo(i.email);
                    myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                    myMessage.Subject = "Service Request Acceptance!!";
                    string url = "http://localhost:14703/ContractorAcceptedBids/HomeownerConfirmation/" + Invoice;
                    //string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href =" + url + "> Click Here </a>";
                    String message = "Hello " + i.FirstName + "," + "<br>" + "<br>" + "Contractor " + ConUserName + " has offered to perform your following service request:" + "<br>" + "<br>" + i.Description + "<br>" + "<br>" + "To confirm acceptance, click on link below: <br><a href =" + url + "> Click Here </a>";
                    myMessage.Html = message;
                    var credentials = new NetworkCredential(name, pass);
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    conList.Add(ConEmail + i.ID);
                }
                //i.posted = true;
                db.SaveChanges();
            }

            return RedirectToAction("About", "Home");
        }

        public ActionResult sendContractor(int id)
        {

            //if (this.User.IsInRole"conr"){

            //}
            //}
            string HomeEmail = "";
            string HomeUserName = "";
            string HomeFirstName = "";
            string HomeLastName = "";
            string HomeAddress = "";
            string HomeCity = "";
            string HomeState = "";
            string HomeZip = "";
            int Invoice = 1;
            //string JobLocation = "";

            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            var contractors = db.Contractors.ToList();
            var homeowners = db.Homeowners.ToList();
            var servicerequests = db.ServiceRequests.ToList();
            var acceptList = db.ContractorAcceptedBids.ToList();
            var confirmedList = db.HomeownerComfirmedBids.ToList();
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();
            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    HomeEmail = user.Email;
                    //username1 = user.UserName;
                    foreach (var home in homeowners)
                    {
                        if (home.email == HomeEmail)
                        {
                            HomeUserName = home.Username;
                            HomeFirstName = home.FirstName;
                            HomeLastName = home.LastName;
                            HomeAddress = home.Address;
                            HomeCity = home.City;
                            HomeState = home.State;
                            HomeZip = home.Zip;
                        }
                    }
                }

            }



            foreach (var i in acceptList)
            {

                if (id == i.ID)
                {

                    HomeownerComfirmedBids bid = new HomeownerComfirmedBids();
                    bid.ServiceRequestPaths = new List<ServiceRequestPath>();
                    bid.ConUsername = i.ConUsername;
                    bid.HomeUsername = i.HomeUsername;
                    bid.ConFirstName = i.ConFirstName;
                    bid.HomeFirstname = i.HomeFirstname;
                    bid.ConLastName = i.ConLastName;
                    bid.HomeLastName = i.HomeLastName;
                    bid.ConAddress = i.ConAddress;
                    bid.HomeAddress = i.HomeAddress;
                    bid.ConCity = i.ConCity;
                    bid.HomeCity = i.HomeCity;
                    bid.ConState = i.ConState;
                    bid.HomeState = i.HomeState;
                    bid.ConZip = i.ConZip;
                    bid.HomeZip = i.HomeZip;
                    bid.ConEmail = i.ConEmail;
                    bid.HomeEmail = i.HomeEmail;
                    bid.PostedDate = i.PostedDate;
                    bid.CompletionDeadline = i.CompletionDeadline;
                    bid.Description = i.Description;
                    bid.Bid = i.Bid;
                    bid.Files = i.Files;
                    bid.Service_Number = i.ServiceNumber;
                    foreach (var x in confirmedList)
                    {
                        if (x.Service_Number == bid.Service_Number)
                        {
                            return RedirectToAction("Already_Confirmed", "HomeOwnerComfirmedBids");
                        }
                    }
                    foreach (var z in servicerequests)
                    {
                        if (z.ServiceNumber == bid.Service_Number)
                        {
                            z.Confirmed = true;
                        }
                    }
                    foreach (var x in i.ServiceRequestPaths)
                    {


                        bid.ServiceRequestPaths.Add(new ServiceRequestPath() { FileName = x.FileName });


                    }
                    bid.expired = i.expired;
                    bid.Completed = false;
                    //bid.Service_Number = i.ServiceNumber;
                    bid.JobLocation = bid.HomeAddress + ", " + bid.HomeCity + ", " + bid.HomeState + " " + bid.HomeZip + ", " + "USA";
                    //foreach (var x in confirmedList)
                    //{
                    //    if (x.Service_Number == bid.Service_Number) 
                    //    {
                    //        return RedirectToAction("Already_Confirmed", "HomeOwnerComfirmedBids");
                    //    }
                    //}
                    //foreach(var z in servicerequests)
                    //{
                    //    if(z.ServiceNumber == bid.Service_Number)
                    //    {
                    //        z.Confirmed = true;
                    //    }
                    //}
                    i.Confirmed = true;
                    bid.Confirmed = true;
                    db.HomeownerComfirmedBids.Add(bid);
                    db.SaveChanges();
                    Invoice = bid.ID;
                    bid.Invoice = Invoice;
                    myMessage.AddTo(i.ConEmail);
                    myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                    myMessage.Subject = "Homeowner Confirmed Your Service!!";
                    string url = "http://localhost:14703/HomeownerComfirmedBids/Confirm/" + Invoice;
                    string url2 = "http://localhost:14703/Maps/Calculate/" + Invoice;
                    //string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href =" + url + "> Click Here </a>";
                    String message = "Hello " + i.ConFirstName + "," + "<br>" + "<br>" + "Homeowner " + HomeUserName + " has confirmed your service for the following request:" + "<br>" + "<br>" + i.Description + "<br>" + "<br>" + "When the job is complete, please confirm completion by clicking on the link below: <br><a href =" + url + "> Click Here </a>" + "<br>" + "<br>" + "Get directions by clicking on the link below: <br><a href =" + url2 + "> Click Here </a>";
                    myMessage.Html = message;
                    var credentials = new NetworkCredential(name, pass);
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    
                }
                //i.posted = true;
                db.SaveChanges();
            }

            return RedirectToAction("About", "Home");
        }

        public ActionResult confirmCompletion(int id)
        {

            //if (this.User.IsInRole"conr"){

            //}
            //}
            string ConEmail = "";
            string ConUserName = "";
            string ConFirstName = "";
            string ConLastName = "";
            string ConAddress = "";
            string ConCity = "";
            string ConState = "";
            string ConZip = "";
            int Invoice = 1;

            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            var contractors = db.Contractors.ToList();
            var homeowners = db.Homeowners.ToList();
            var servicerequests = db.ServiceRequests.ToList();
            var acceptList = db.ContractorAcceptedBids.ToList();
            var confirmedList = db.HomeownerComfirmedBids.ToList();
            var completedList = db.CompletedBids.ToList();
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var person = db.Contractors.Where(x => x.UserId == identity).SingleOrDefault();
            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    ConEmail = user.Email;
                    //username1 = user.UserName;
                    foreach (var con in contractors)
                    {
                        if (con.email == ConEmail)
                        {
                            ConUserName = con.Username;
                            ConFirstName = con.FirstName;
                            ConLastName = con.LastName;
                            ConAddress = con.Address;
                            ConCity = con.City;
                            ConState = con.State;
                            ConZip = con.Zip;
                        }
                    }
                }

            }



            foreach (var i in confirmedList)
            {

                if (id == i.ID)
                {

                    CompletedBids bid = new CompletedBids();
                    bid.ServiceRequestPaths = new List<ServiceRequestPath>();
                    bid.AfterPaths = new List<AfterPath>();
                    bid.CompletedPaths = new List<CompletedPath>();
                    bid.ConUsername = i.ConUsername;
                    bid.HomeUsername = i.HomeUsername;
                    bid.ConFirstName = i.ConFirstName;
                    bid.HomeFirstname = i.HomeFirstname;
                    bid.ConLastName = i.ConLastName;
                    bid.HomeLastName = i.HomeLastName;
                    bid.ConAddress = i.ConAddress;
                    bid.HomeAddress = i.HomeAddress;
                    bid.ConCity = i.ConCity;
                    bid.HomeCity = i.HomeCity;
                    bid.ConState = i.ConState;
                    bid.HomeState = i.HomeState;
                    bid.ConZip = i.ConZip;
                    bid.HomeZip = i.HomeZip;
                    bid.ConEmail = i.ConEmail;
                    bid.HomeEmail = i.HomeEmail;
                    bid.PostedDate = i.PostedDate;
                    bid.CompletionDeadline = i.CompletionDeadline;
                    bid.Description = i.Description;
                    bid.Bid = i.Bid;
                    bid.Files = i.Files;
                    bid.Service_Number = i.Service_Number;
                    foreach (var x in completedList)
                    {
                        if (x.Service_Number == bid.Service_Number)
                        {
                            return RedirectToAction("Already_Confirmed_Completion", "CompletedBids");
                        }
                    }
                    foreach (var x in i.ServiceRequestPaths)
                    {


                        bid.ServiceRequestPaths.Add(new ServiceRequestPath() { FileName = x.FileName });


                    }
                    foreach (var x in i.AfterPaths)
                    {
                        bid.CompletedPaths.Add(new CompletedPath() { FileName = x.FileName });
                    }
                    //bid.AfterPaths = i.AfterPaths;
                    //foreach (var x in i.AfterPaths)
                    //{


                    //    bid.AfterPaths.Add(new AfterPath() { FileName = x.FileName });
                    //    bid. = homeownerComfirmedBids.ID;

                    //}
                    //bid.Service_Number = i.Service_Number;
                    bid.ContractorDue = i.Bid * .9m;
                    bid.ContractorPaid = false;
                    //foreach (var x in completedList)
                    //{
                    //    if (x.Service_Number == bid.Service_Number)
                    //    {
                    //        return RedirectToAction("Already_Confirmed_Completion", "CompletedBids");
                    //    }
                    //}

                    bid.Completed = true;
                    db.CompletedBids.Add(bid);
                    i.Completed = true;
                    db.SaveChanges();
                    Invoice = bid.ID;
                    bid.Invoice = Invoice;
                    myMessage.AddTo(i.HomeEmail);
                    myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                    myMessage.Subject = "Job Complete!!";
                    string url = "http://localhost:14703/CompletedBids/Payment/" + Invoice;
                    //string message = "Job Location: <br>" + i.Address + "<br>" + i.City + "<br>" + i.State + "<br>" + i.Zip + "<br>" + "<br>" + "Job Description: <br>" + i.Description + "<br>" + "<br>" + "Bid price: <br>$" + i.Bid + "<br>" + "<br>" + "Must be completed by: <br>" + i.CompletionDeadline + "<br>" + "<br>" + "Date Posted: <br>" + i.PostedDate + "<br>" + "<br>" + "To accept job, click on link below: <br><a href =" + url + "> Click Here </a>";
                    String message = "Hello " + i.HomeFirstname + "," + "<br>" + "<br>" + "Contractor " + ConUserName + " has completed your following service request:" + "<br>" + "<br>" + i.Description + "<br>" + "<br>" + "To complete payment, please click on link below: <br><a href =" + url + "> Click Here </a>";
                    myMessage.Html = message;
                    var credentials = new NetworkCredential(name, pass);
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    conList.Add(ConEmail + i.ID);
                }
                //i.posted = true;
                db.SaveChanges();
            }

            return RedirectToAction("About", "Home");
        }

        public ActionResult Post_must_be_logged()
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
