using System;
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
using Quartz;


namespace WorkWarriors
{
    public class Quartz : IJob
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Execute(IJobExecutionContext context)
        {
            db = new ApplicationDbContext();
            var myMessage = new SendGrid.SendGridMessage();
            var requests = db.ServiceRequests.ToList();
            string name = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\name.txt");
            string pass = System.IO.File.ReadAllText(@"C:\Users\erick\Desktop\Credentials\password.txt");
            foreach (var i in requests)
            {
                if ((i.CompletionDeadline < DateTime.Now) && (i.expired == false))
                {
                    myMessage.AddTo(i.email);
                    myMessage.From = new MailAddress("monsymonster@msn.com", "Joe Johnson");
                    myMessage.Subject = "Your service request has expired!";
                    string message = "Hello " + i.Username + "," + "<br>" + "<br>" + "Your service request \"" + i.Description + "\" has expired because the completion deadline has passed.";
                    myMessage.Html = message;
                    var credentials = new NetworkCredential(name, pass);
                    var transportWeb = new SendGrid.Web(credentials);
                    transportWeb.DeliverAsync(myMessage);
                    i.expired = true;
                    db.SaveChanges();
                }
               
            }
        }
    }
}

