using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWarriors.Models
{
    public class ServiceRequest
    {
        public int ID { get; set; }
        public virtual Homeowner Homeowner { get; set; }
        //[StringLength(15, MinimumLength = 6)]
        //[Required]
        public string Username { get; set; }
       // [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }
       // [StringLength(25, MinimumLength = 1)]
        public string LastName { get; set; }
        [StringLength(40, MinimumLength = 6)]
        [Required]
        public string Address { get; set; }
        [StringLength(25, MinimumLength = 1)]
        [Required]
        public string City { get; set; }
        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string State { get; set; }
        [StringLength(5, MinimumLength = 5)]
        [Required]
        public string Zip { get; set; }
        [StringLength(25, MinimumLength = 5)]
        //[Required]
        public string email { get; set; }
       // [Required]
        public DateTime PostedDate { get; set; }
        [Required]
        public decimal Bid { get; set; }
        [Required]
        public DateTime CompletionDeadline { get; set; }
        [StringLength(100, MinimumLength = 6)]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public bool posted { get; set; }
        public bool expired { get; set; }
        public bool vacant { get; set; }

        public bool Confirmed { get; set; }
        public int ServiceNumber { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<ServiceRequestPath> ServiceRequestPaths { get; set; }
        //[ForeignKey("Contractor")]
        //public int ContractorID { get;  set;}
        //public virtual Contractor Contractor { get; set; }
    }
}