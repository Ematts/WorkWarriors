using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SharpShip.UPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using WorkWarriors.Models;

namespace WorkWarriors.Controllers
{
    public class AddressValidatorController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        public ActionResult getView()
        {
            var files = Request.Files;
            var form = Request.Form;
            string town = (form[3]);
            string city = form.AllKeys[3];
            return View();
        }

        [HttpGet]
        public ActionResult getAddValStauts(string street, string city, string state, string zip)
        {
            AddValStatus addValStatus = new AddValStatus();
            //if (serviceRequest.vacant == true)
            //{
            //    string UPS = RunStreetLevelValidation(serviceRequest);
            //    if (UPS == "false")
            //    {
            //        addValStatus.status = "false";
            //        return addValStatus;
            //    }
            //    addValStatus.status = "true";
            //    return addValStatus;
            //}
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            EasyPost.ClientManager.SetCurrent("wGW1bI8SYpamubvkDKNkFw");
            EasyPost.Address address = new EasyPost.Address()
            {
                company = "",
                street1 = street,
                street2 = "",
                city = city,
                state = state,
                country = "US",
                zip = zip,
                verify = new List<string>() { "delivery" }
            };
            if (address == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            address.Create();

            if (address.verifications.delivery.success)
            {

                addValStatus.status = "true";
                return Json(new { success = true, street = street, validated = address.verifications.delivery.success },
               JsonRequestBehavior.AllowGet); 
            }

            else //else if (address.verifications.delivery.errors.Count == 1 && address.verifications.delivery.errors[0].message == "Address not found")
            {
                return Json(new { success = true, street = street, validated = address.verifications.delivery.success, errors = address.verifications.delivery.errors },
               JsonRequestBehavior.AllowGet); 
            }

            //else if (address.verifications.delivery.errors.Count == 1 && address.verifications.delivery.errors[0].message == "Address not found")
            //{
            //    string UPS = RunStreetLevelValidation(street, city, state, zip );
            //    if (UPS == "false")
            //    {
            //        addValStatus.status = "false";
            //        return null;
            //    }
            //    addValStatus.status = "true";
            //    return null;
            //}

            //else if (address.verifications.delivery.errors.Count > 0)
            //{
            //    List<string> errorsList = new List<string>();
            //    for (int i = 0; i < address.verifications.delivery.errors.Count; i++)
            //    {
            //        errorsList.Add(address.verifications.delivery.errors[i].message);
            //    }
            //    addValStatus.status = "errors";
            //    addValStatus.errorList = errorsList;
            //    return null;

            //}

            //else
            //{
            //    addValStatus = null;
            //    return null;
            //}


        }
        [HttpGet]
        public ActionResult RunStreetLevelValidation(string street, string city, string state, string zip)
        {


            StreetLevelAddressValidator validator = new StreetLevelAddressValidator("7D15274E4E2A07DE", "Honeybump20!", "pennywise79");
            SharpShip.Entities.Address myAddress = new SharpShip.Entities.Address()
            {
                AddressLine1 = street,
                City = city,
                StateProvince = state,
                CountryCode = "US",
                PostalCode = zip
            };

            List<SharpShip.Entities.Address> results = new List<SharpShip.Entities.Address>();
            var isvalid = validator.Validate(myAddress, ref results);


            if (isvalid == true && results.Count > 0)
            {
                return Json(new { success = true, street = street, validated = isvalid, results = results },
               JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = true, street = street, validated = false },
               JsonRequestBehavior.AllowGet);
            }
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
        [HttpGet]
        public ActionResult ManualValidation(string street, string city, string state, string zip, bool vacant, bool validated)
        {
            Address address = new Address() {Street = street, City = city, State = state, Zip = zip, vacant = vacant, validated = validated };
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();
            if (identity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(TryValidateModel(address) == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            address.UserId = identity;
            db.Addresses.Add(address);
            db.SaveChanges();
            return Json(new { success = true, street = street },
              JsonRequestBehavior.AllowGet);
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