using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class AddValStatus
    {
        public string status { get; set; }
        public ICollection<string> errorList { get; set; }
    }
}