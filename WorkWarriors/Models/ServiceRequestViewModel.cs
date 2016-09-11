using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class ServiceRequestViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string FullAddress
        {
            get { return Address + ", " + City + ", " + State + " " + Zip; }
        }

        public ICollection<string> errorList { get; set; }

    }
}