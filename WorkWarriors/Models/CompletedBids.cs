﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorkWarriors.Models
{
    public class CompletedBids
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

        public decimal Bid { get; set; }

        public DateTime CompletionDeadline { get; set; }
        public string Description { get; set; }

        public bool Completed { get; set; }
        public int Invoice { get; set; }
        public decimal ContractorDue { get; set; }
        public bool ContractorPaid { get; set; }
        public int Service_Number { get; set; }
        public bool Expired { get; set; }
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }
        [Range(0, 5)]
        public double Rating { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<ServiceRequestPath> ServiceRequestPaths { get; set; }
        public virtual ICollection<AfterPath> AfterPaths { get; set; }
        public virtual ICollection<CompletedPath> CompletedPaths { get; set; }
    }
}