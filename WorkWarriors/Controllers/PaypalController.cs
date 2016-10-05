
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWarriors.Models;
using log4net.Core;
using log4net;
using PayPal.AdaptivePayments.Model;
using PayPal.AdaptivePayments;
using System.Text;
using System.Net;
using System.IO;

namespace WorkWarriors.Controllers
{
    public class PaypalController : Controller
    {

        public ActionResult Index(int? id)
        {
            //decimal price = 50;
            //Bids bid = db.Bids.Find(id);
            ReceiverList receiverList = new ReceiverList();
            receiverList.receiver = new List<Receiver>();
            Receiver receiver = new Receiver(50);
            //var query = from v in db.Ventures where v.Id == bid.ventureID select v.investorID;
            //string receiverID = query.ToList().ElementAt(0);
            //ApplicationUser recvUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(receiverID.ToString());
            receiver.email = "workwarriors@gmail.com";
            receiver.primary = true;
            receiverList.receiver.Add(receiver);
            Receiver receiver2 = new Receiver(10);
            //var query = from v in db.Ventures where v.Id == bid.ventureID select v.investorID;
            //string receiverID = query.ToList().ElementAt(0);
            //ApplicationUser recvUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(receiverID.ToString());
            receiver2.email = "carlcontractor@gmail.com";
            receiver2.primary = false;
            receiverList.receiver.Add(receiver2);

            RequestEnvelope requestEnvelope = new RequestEnvelope("en_US");
            string actionType = "PAY";

            string successUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/PayPal/SuccessView/{0}";
            string failureUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/PayPal/FailureView/{0}";
            successUrl = String.Format(successUrl, id);
            failureUrl = String.Format(failureUrl, id);
            string returnUrl = successUrl;
            string cancelUrl = failureUrl;

            string currencyCode = "USD";
            PayRequest payRequest = new PayRequest(requestEnvelope, actionType, cancelUrl, currencyCode, receiverList, returnUrl);
            payRequest.ipnNotificationUrl = "http://de3b6191.ngrok.io";

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
            SimpleListenerExample("localhost: 80");
            return View();
        }
        [HttpPost]
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
         [HttpPost]
        public static void SimpleListenerExample(string prefix)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefix == null || prefix.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.

            listener.Prefixes.Add(prefix);
            
            listener.Start();
            Console.WriteLine("Listening...");
            // Note: The GetContext method blocks while waiting for a request. 
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            listener.Stop();
        }
        public ActionResult FailureView(int? id)
        {

            return View();

        }
        public ActionResult SuccessView(int? id)
        {

            return View();
        }
    }
}


