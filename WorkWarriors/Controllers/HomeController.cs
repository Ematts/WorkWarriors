using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;

namespace WorkWarriors.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(PayPalCheckoutInfo payPalCheckoutInfo)
        {
            PayPalListenerModel model = new PayPalListenerModel();
            model._PayPalCheckoutInfo = payPalCheckoutInfo;
            byte[] parameters = Request.BinaryRead(Request.ContentLength);

            if (parameters != null && parameters.Length > 0)
            {
                model.GetStatus(parameters);
                db.PayPalListenerModels.Add(model);
                string request = model._PayPalCheckoutInfo.memo;
                foreach (var z in db.PayPalListenerModels.ToList())
                {
                    if (z._PayPalCheckoutInfo.txn_id == model._PayPalCheckoutInfo.txn_id)
                    {
                        db.PayPalListenerModels.Remove(model);
                    }
                       
                }
                db.SaveChanges();
                return new EmptyResult();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InvalidLogin()
        {
            ViewBag.Message = "Invalid Login Attempt.";

            return View();
        }
        public ActionResult Unauthorized_Access()
        {
            ViewBag.Message = "You are not authorized to view this page.";

            return View();
        }
        public ActionResult View_Databases()
        {
            ViewBag.Message = "Select Database";

            return View();
        }

        [AllowAnonymous]
        public ActionResult PayPalPaymentNotification(PayPalCheckoutInfo payPalCheckoutInfo)
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

        [AllowAnonymous]
        public ActionResult PayPalPaymentNotification1()
        {
            string strLog = "";
            string currentTime = "";


            currentTime = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year + "|" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

            strLog = "Insert into CPLog(Log,LogTime) values('Start IPN request','" + currentTime + "')";
           // InsertData(strLog);
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] Param = Request.BinaryRead(HttpContext.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(Param);
            strRequest = strRequest + "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //Dim proxy As New WebProxy(New System.Uri("http://url:port#"))
            //req.Proxy = proxy

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct
                //process payment
                currentTime = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year + "|" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                strLog = "Insert into CPLog(Log,LogTime) values('Status - Verified','" + currentTime + "')";
                //InsertData(strLog);
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
                currentTime = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year + "|" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                strLog = "Insert into CPLog(Log,LogTime) values('Status - Invalid','" + currentTime + "')";
                //InsertData(strLog);
            }
            else
            {
                //Response wasn't VERIFIED or INVALID, log for manual investigation
            }
            currentTime = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year + "|" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

            strLog = "Insert into CPLog(Log,LogTime) values('Finish IPN Request','" + currentTime + "')";
            //InsertData(strLog);
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