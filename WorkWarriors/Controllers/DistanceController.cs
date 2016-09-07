using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WorkWarriors.Controllers
{
    public class DistanceController : Controller
    {
        // GET: Distance
        public ActionResult getDistance()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GetDistanceTest()
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(
              "https://maps.googleapis.com/maps/api/distancematrix/json?origins=Milwaukee+WI&destinations=walker+MN&units=imperial&key=AIzaSyAZN5FYYDCim12U0c6Emy-MoRr6AGbmO9E");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
            return RedirectToAction ("Test", "Distance");
        }
    }
}

//https://maps.googleapis.com/maps/api/js?key=AIzaSyAZN5FYYDCim12U0c6Emy-MoRr6AGbmO9E&callback=initMap