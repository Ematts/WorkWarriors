﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkWarriors.Models
{
    public class Homeowner
    {
        public int ID { get; set; }
        [StringLength(15, MinimumLength = 6)]
        [Required]
        //[Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string Username { get; set; }
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(25, MinimumLength = 1)]
        [Required]
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
        [Required]
        public string email { get; set; }
    }

    
}