﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;
using Microsoft.AspNet.Identity;
using PayPal.AdaptivePayments.Model;
using PayPal.AdaptivePayments;
using PayPal.Api;
using System.Net.Mail;

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
            CompletedBids Pics = db.CompletedBids.Include(i => i.ServiceRequestPaths).SingleOrDefault(i => i.ID == id);
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
        public ActionResult Create([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice,ConstactorPaid,ConstactorDue,Expired,Service_Number")] CompletedBids completedBids, HttpPostedFileBase upload)
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

        public ActionResult Add_Review(int? id)
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
        public ActionResult Edit([Bind(Include = "ID,ConUsername,HomeUsername,ConFirstName,HomeFirstname,ConLastName,HomeLastName,ConAddress,HomeAddress,ConCity,HomeCity,ConState,HomeState,ConZip,HomeZip,ConEmail,HomeEmail,PostedDate,Bid,CompletionDeadline,Description,Completed,Invoice,ContractorPaid,ContractorDue,Expired,Service_Number,Review,Rating")] CompletedBids completedBids)
        {
            if (completedBids.Review == null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(completedBids).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(completedBids);
            }

            if (ModelState.IsValid)
            {
                Reviews review = new Reviews();
                review.ConUsername = completedBids.ConUsername;
                review.HomeUsername = completedBids.HomeUsername;
                review.ConFirstName = completedBids.ConFirstName;
                review.HomeFirstname = completedBids.HomeFirstname;
                review.ConLastName = completedBids.ConLastName;
                review.HomeLastName = completedBids.HomeLastName;
                review.ConAddress = completedBids.ConAddress;
                review.HomeAddress = completedBids.HomeAddress;
                review.ConCity = completedBids.ConCity;
                review.HomeCity = completedBids.HomeCity;
                review.ConState = completedBids.ConState;
                review.HomeState = completedBids.HomeState;
                review.ConZip = completedBids.ConZip;
                review.HomeZip = completedBids.HomeZip;
                review.ConEmail = completedBids.ConEmail;
                review.HomeEmail = completedBids.HomeEmail;
                review.PostedDate = completedBids.PostedDate;
                review.Bid = completedBids.Bid;
                review.CompletionDeadline = completedBids.CompletionDeadline;
                review.Description = completedBids.Description;
                review.Completed = completedBids.Completed;
                review.Invoice = completedBids.Invoice;
                review.ContractorDue = completedBids.ContractorDue;
                review.ContractorPaid = completedBids.ContractorPaid;
                review.Service_Number = completedBids.Service_Number;
                review.Expired = completedBids.Expired;
                review.Review = completedBids.Review;
                review.Rating = completedBids.Rating;
                db.Reviews.Add(review);
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
                if (id == i.ID)
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
                ReceiverList receiverList = new ReceiverList();
                receiverList.receiver = new List<Receiver>();
                Receiver receiver = new Receiver(completedBids.Bid);
                //var query = from v in db.Ventures where v.Id == bid.ventureID select v.investorID;
                //string receiverID = query.ToList().ElementAt(0);
                //ApplicationUser recvUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(receiverID.ToString());
                receiver.email = "workwarriors@gmail.com";
                receiver.primary = true;
                receiverList.receiver.Add(receiver);
                Receiver receiver2 = new Receiver(completedBids.ContractorDue);
                //var query = from v in db.Ventures where v.Id == bid.ventureID select v.investorID;
                //string receiverID = query.ToList().ElementAt(0);
                //ApplicationUser recvUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(receiverID.ToString());
                receiver2.email = completedBids.ConEmail;
                receiver2.primary = false;
                receiverList.receiver.Add(receiver2);
                RequestEnvelope requestEnvelope = new RequestEnvelope("en_US");
                string actionType = "PAY";
                string successUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/CompletedBids/SuccessView/{0}";
                string failureUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/CompletedBids/FailureView/{0}";
                successUrl = String.Format(successUrl, id);
                failureUrl = String.Format(failureUrl, id);
                string returnUrl = successUrl;
                string cancelUrl = failureUrl;
                string currencyCode = "USD";
                PayRequest payRequest = new PayRequest(requestEnvelope, actionType, cancelUrl, currencyCode, receiverList, returnUrl);
                payRequest.ipnNotificationUrl = "http://cf719b5f.ngrok.io";
                string memo = completedBids.Description + " Invoice = " + completedBids.Invoice;
                payRequest.memo = memo;
                Dictionary<string, string> sdkConfig = new Dictionary<string, string>();
                sdkConfig.Add("mode", "sandbox");
                sdkConfig.Add("account1.apiUsername", "mattjheller-facilitator_api1.yahoo.com"); //PayPal.Account.APIUserName
                sdkConfig.Add("account1.apiPassword", "DG6GB55TRBWLESWG"); //PayPal.Account.APIPassword
                sdkConfig.Add("account1.apiSignature", "AFcWxV21C7fd0v3bYYYRCpSSRl31AafAKKwBsAp2EBV9PExGkablGWhj"); //.APISignature
                sdkConfig.Add("account1.applicationId", "APP-80W284485P519543T"); //.ApplicatonId

                AdaptivePaymentsService adaptivePaymentsService = new AdaptivePaymentsService(sdkConfig);
                PayResponse payResponse = adaptivePaymentsService.Pay(payRequest);
                ViewData["paykey"] = payResponse.payKey;

                //string payKey = payResponse.payKey; ////////
                //string paymentExecStatus = payResponse.paymentExecStatus;
                //string payURL = String.Format("https://www.sandbox.paypal.com/webscr?cmd=_ap-payment&paykey={0}", payKey);
                return View(completedBids);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }

        
        public EmptyResult PayPalPaymentNotification(PayPalCheckoutInfo payPalCheckoutInfo)
        {
            PayPalListenerModel model = new PayPalListenerModel();
            model._PayPalCheckoutInfo = payPalCheckoutInfo;
            byte[] parameters = Request.BinaryRead(Request.ContentLength);

            if (parameters != null)
            {
                model.GetStatus(parameters);
            }

            return new EmptyResult();
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

        public ActionResult MarkAsPaid(int? id)
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

            completedBids.ContractorPaid = true;
            db.SaveChanges();

            return View("PayContractor", completedBids);
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


        public ActionResult FailureView(int? id)
        {

            return View();

        }
        public ActionResult SuccessView(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            CompletedBids completedBids = db.CompletedBids.Find(id);

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
                if (id == i.ID)
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

                if (completedBids == null)
                {
                    return HttpNotFound();
                }

                if (completedBids.ContractorPaid == true)
                {
                    return RedirectToAction("Paid", new  { id = completedBids.ID } );
                }

                completedBids.ContractorPaid = true;
                db.SaveChanges();
                var myMessage = new SendGrid.SendGridMessage();
                string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
                string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
                myMessage.AddTo(completedBids.HomeEmail);
                myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
                myMessage.Subject = "Job Complete!!";
                string url = "http://localhost:14703/CompletedBids/Add_Review/" + completedBids.ID;
                String message = "Hello " + completedBids.HomeFirstname + "," + "<br>" + "<br>" + "Thank you for using Work Warriors!  To review " + completedBids.ConUsername + "'s service, please click on link below: <br><a href =" + url + "> Click Here </a>"; 
                myMessage.Html = message;
                var credentials = new NetworkCredential(name, pass);
                var transportWeb = new SendGrid.Web(credentials);
                transportWeb.DeliverAsync(myMessage);
                ConPaidMail(completedBids.ConEmail, completedBids.ConUsername, completedBids.ContractorDue);

            }

            return View(completedBids);
        }

        public static void ConPaidMail(string ConEmail, string ConUsername, decimal Amount)
        {
            var myMessage = new SendGrid.SendGridMessage();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            myMessage.AddTo(ConEmail);
            myMessage.From = new MailAddress("workwarriors@gmail.com", "Admin");
            myMessage.Subject = "Job Complete!!";
            String message = "Hello " + ConUsername + "," + "<br>" + "<br>" + "Thank you for using Work Warriors! $" + Amount + " has been paid to your account!";
            myMessage.Html = message;
            var credentials = new NetworkCredential(name, pass);
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }

        public ActionResult Paid(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            CompletedBids completedBids = db.CompletedBids.Find(id);
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
                if (id == i.ID)
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

                if (completedBids == null)
                {
                    return HttpNotFound();
                }

              
            }
            return View(completedBids);
        }
    }
}



