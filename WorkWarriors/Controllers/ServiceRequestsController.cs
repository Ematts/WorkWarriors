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
using System.Globalization;
using System.IO;
using EasyPost;
using SharpShip.UPS;
using SharpShip.Entities;

namespace WorkWarriors.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceRequests
        public ActionResult Index()
        {
            return View(db.ServiceRequests.ToList());
        }

        // GET: ServiceRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            ServiceRequest serviceRequestPic = db.ServiceRequests.Include(i => i.ServiceRequestPaths).SingleOrDefault(i => i.ID == id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email,PostedDate,Bid,CompletionDeadline,Description,posted,Contractor,vacant")] ServiceRequest serviceRequest, IEnumerable<HttpPostedFileBase> files, string street, string number)
        {

            //serviceRequest.Address = number + " " + street;

            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            
            if (identity == null)
            {
                return RedirectToAction("Must_be_logged_in", "ServiceRequests");
            }

            if (!this.User.IsInRole("Admin") && (!this.User.IsInRole("Homeowner")))
            {
                return RedirectToAction("Must_be_logged_in", "ServiceRequests");
            }

            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    serviceRequest.email = user.Email;
                    serviceRequest.Username = user.Screen_Name;
                    serviceRequest.FirstName = user.First_Name;
                    serviceRequest.LastName = user.Last_Name;
                    serviceRequest.PostedDate = DateTime.Now;
                }
            }


            if(serviceRequest.PostedDate > serviceRequest.CompletionDeadline)
            {
                return RedirectToAction("DateIssue", "ServiceRequests");
            }

            serviceRequest.ServiceRequestPaths = new List<ServiceRequestPath>();
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {

                    if (file != null && file.ContentLength > 0)
                    {


                        var photo = new ServiceRequestPath() { FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName) };
                        file.SaveAs(Path.Combine(Server.MapPath("~/images"), photo.FileName));
                        //movie.FilePaths = new List<FilePath>();
                        serviceRequest.ServiceRequestPaths.Add(photo);
                    }

                }
                AddValStatus addValStatus = getAddValStauts(serviceRequest);
                if (addValStatus == null)
                {
                    return HttpNotFound();
                }
                if (addValStatus.status == "false")
                {
                    return RedirectToAction("NoAddress", "ServiceRequests");
                }
                if (addValStatus.status == "errors")
                {
                    TempData["addValStatus"] = addValStatus;
                    TempData["serviceRequest"] = serviceRequest;
                    return RedirectToAction("Errors", "ServiceRequests", new { addValStatus = TempData["addValStatus"], serviceRequest = TempData["serviceRequest"] });
                }
                db.ServiceRequests.Add(serviceRequest);
                db.SaveChanges();
                return RedirectToAction("Post", new { id = serviceRequest.ID });


            }

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,FirstName,LastName,Address,City,State,Zip,email,PostedDate,Bid,CompletionDeadline,Description,posted,contractor,ServiceNumber,Expired,Confirmed")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(serviceRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult NoAddress()
        {
            return View();
        }
        public ActionResult ContractorAcceptance(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var serviceList = db.ServiceRequests.ToList();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }

            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();

            if (this.User.IsInRole("Admin") || this.User.IsInRole("Contractor"))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
                ServiceRequest serviceRequestPic = db.ServiceRequests.Include(i => i.ServiceRequestPaths).SingleOrDefault(i => i.ID == id);
                if (serviceRequest == null)
                {
                    return HttpNotFound();
                }

                if (serviceRequest.expired == true)
                {
                        return RedirectToAction("expired", "ServiceRequests");
                }

                return View(serviceRequest);
                }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }
        public ActionResult Somemethod(bool isTrue)
        {
            if (isTrue)
            {
                //do something
            }
            else
            {
                //do something
            }
            return View();
        }
        public ActionResult Errors(AddValStatus addValStatus, ServiceRequest serviceRequest)
        {
            addValStatus = (AddValStatus)TempData["addValStatus"];
            serviceRequest = (ServiceRequest)TempData["serviceRequest"];
            if (addValStatus == null || serviceRequest == null)
            {
                return HttpNotFound();
            }
            ServiceRequestViewModel model = new ServiceRequestViewModel();
            model.errorList = new List<string>();
            model.Address = serviceRequest.Address;
            model.City = serviceRequest.City;
            model.State = serviceRequest.State;
            model.Zip = serviceRequest.Zip;
            foreach(var error in addValStatus.errorList)
            {
                model.errorList.Add(error);
            }
            TempData["addValStatus"] = addValStatus;
            TempData["serviceRequest"] = serviceRequest;

            return View(model);
        }
        public ActionResult Post(int? id)
        {
            string identity = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (identity == null)
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
            var serviceList = db.ServiceRequests.ToList();
            string HomeOwnerEmail1 = "";
            string HomeOwnerEmail2 = "";
            var person = db.Homeowners.Where(x => x.UserId == identity).SingleOrDefault();


            foreach (var user in db.Users)
            {
                if (user.Id == identity)
                {
                    HomeOwnerEmail2 = user.Email;
                }
            }

            foreach (var i in serviceList)
            {
                if (id == i.ID)
                {
                    HomeOwnerEmail1 = i.email;
                }
            }

            if (this.User.IsInRole("Admin") || HomeOwnerEmail1 == HomeOwnerEmail2)
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
                ServiceRequest serviceRequestPic = db.ServiceRequests.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
                if (serviceRequest == null)
                {
                    return HttpNotFound();
                }
                return View(serviceRequest);
            }
            else
            {
                return RedirectToAction("Unauthorized_Access", "Home");
            }
        }

        public ActionResult Must_be_logged_in()
        {
            ViewBag.Message = "You must log in as a registered homeowner to create a service request";

            return View();
        }

        public ActionResult DateIssue()
        {
            ViewBag.Message = "Completion deadline must be later than posted date";

            return View();
        }

        public ActionResult Expired()
        {
            ViewBag.Message = "This service request has expired because the completion deadline has passed.";

            return View();
        }

        public ActionResult DuplicatePost()
        {
            ViewBag.Message = "This service request has already been posted.";

            return View();
        }
        public ActionResult Validate()
        {
            ViewBag.Message = "This service request has already been posted.";

            return View();
        }

        public AddValStatus getAddValStauts(ServiceRequest serviceRequest)
        {
            AddValStatus addValStatus = new AddValStatus();
            if (serviceRequest.vacant == true)
            {
                string UPS = RunStreetLevelValidation(serviceRequest);
                if (UPS == "false")
                {
                    addValStatus.status = "false";
                    return addValStatus;
                }
                addValStatus.status = "true";
                return addValStatus;
            }
            
            EasyPost.ClientManager.SetCurrent("wGW1bI8SYpamubvkDKNkFw");
            EasyPost.Address address = new EasyPost.Address()
            {
                company = "",
                street1 = serviceRequest.Address,
                street2 = "",
                city = serviceRequest.City,
                state = serviceRequest.State,
                country = "US",
                zip = serviceRequest.Zip,
                verify = new List<string>() { "delivery" }
            };

            address.Create();

            if (address.verifications.delivery.success)
            {

                addValStatus.status = "true";
                return addValStatus;
            }

            else if (address.verifications.delivery.errors.Count == 1 && address.verifications.delivery.errors[0].message == "Address not found")
            {
                string UPS = RunStreetLevelValidation(serviceRequest);
                if (UPS == "false")
                {
                    addValStatus.status = "false";
                    return addValStatus;
                }
                addValStatus.status = "true";
                return addValStatus;
            }

            else if (address.verifications.delivery.errors.Count > 0) 
            {
                List<string> errorsList = new List<string>();
                for (int i = 0; i < address.verifications.delivery.errors.Count; i++)
                {
                    errorsList.Add(address.verifications.delivery.errors[i].message);
                }
                addValStatus.status = "errors";
                addValStatus.errorList = errorsList;
                return addValStatus;
                
            }

            else
            {
                addValStatus = null;
                return addValStatus;
            }


        }

        public string RunStreetLevelValidation(ServiceRequest serviceRequest)
        {
            StreetLevelAddressValidator validator = new StreetLevelAddressValidator("7D15274E4E2A07DE", "Honeybump20!", "pennywise79");
            SharpShip.Entities.Address myAddress = new SharpShip.Entities.Address()
            {
                AddressLine1 = serviceRequest.Address,
                City = serviceRequest.City,
                StateProvince = serviceRequest.State,
                CountryCode = "US",
                PostalCode = serviceRequest.Zip
            };

            List<SharpShip.Entities.Address> results = new List<SharpShip.Entities.Address>();
            var isvalid = validator.Validate(myAddress, ref results);


            if(isvalid == true && results.Count > 0)
            {
                return "true";
            }

            else
            {
                return "false";
            }
        }





        //public ActionResult HomeownerConfirmation(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
        //    if (serviceRequest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(serviceRequest);
        //}
    }
}
