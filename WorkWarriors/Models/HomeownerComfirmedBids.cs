using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class HomeownerComfirmedBids
    {
        public int ID { get; set; }
        public string ConUsername { get; set; }
        public string HomeUsername { get; set; }
        public string ConFirstName { get; set; }
        public string HomeFirstname { get; set; }
        public string ConLastName { get; set; }
        public string HomeLastName { get; set; }
        public string ConAddress { get; set; }
        public string HomeAddress { get; set; }
        public string ConCity { get; set; }
        public string HomeCity { get; set; }

        public string ConState { get; set; }

        public string HomeState { get; set; }

        public string ConZip { get; set; }
        public string HomeZip { get; set; }

        public string ConEmail { get; set; }
        public string HomeEmail { get; set; }

        public DateTime PostedDate { get; set; }

        public double Bid { get; set; }

        public DateTime CompletionDeadline { get; set; }
        public string Description { get; set; }

        public bool Completed { get; set; }
        public int Invoice { get; set; }
        public string JobLocation { get; set; }
    }
}
    
