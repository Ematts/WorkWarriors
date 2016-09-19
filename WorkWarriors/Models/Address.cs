using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class Address
    {
        public int ID { get; set; }
        [StringLength(40, MinimumLength = 6)]
        [Required]
        public string Street { get; set; }
        [StringLength(40, MinimumLength = 1)]
        [Required]
        public string City { get; set; }
        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string State { get; set; }
        [StringLength(5, MinimumLength = 5)]
        [Required]
        public string Zip { get; set; }
        [Display(Name = "Full Address")]
        public string FullAddress
        {
            get { return Street + ", " + City + ", " + State + " " + Zip; }
        }
        public bool vacant { get; set; }
        public bool validated { get; set; }
        public int? HomeownerID { get; set; }
        [ForeignKey("HomeownerID")]
        public virtual Homeowner homeowner { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}