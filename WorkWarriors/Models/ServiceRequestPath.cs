using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ServiceRequestPath
    {
        public int ServiceRequestPathId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }

        //public int ServiceRequestId { get; set; }
        //[ForeignKey("ServiceRequestId")]

        //public virtual ServiceRequest ServiceRequest { get; set; }
    }
}