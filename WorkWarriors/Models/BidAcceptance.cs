//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;

//namespace WorkWarriors.Models
//{
//    public class BidAcceptance
//    {
//        public int ID { get; set; }
//        public virtual ServiceRequest ServiceRequest { get; set; }
//        [StringLength(40, MinimumLength = 6)]
//        [Required]
//        public string Address { get; set; }
//        [StringLength(25, MinimumLength = 1)]
//        [Required]
//        public string City { get; set; }
//        [StringLength(2, MinimumLength = 2)]
//        [Required]
//        public string State { get; set; }
//        [StringLength(5, MinimumLength = 5)]
//        [Required]
//        public string Zip { get; set; }
//        [Required]
//        public DateTime PostedDate { get; set; }
//        [Required]
//        public double Bid { get; set; }
//        [Required]
//        public DateTime CompletionDeadline { get; set; }
//        [StringLength(100, MinimumLength = 6)]
//        [Required]
//        public string Description { get; set; }
       
//    }
//}