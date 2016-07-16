using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class AfterPath
    {
        public int AfterPathId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int HomeownerComfirmedBidsID { get; set; }
        [ForeignKey("HomeownerComfirmedBidsID")]
        public virtual HomeownerComfirmedBids HomeownerComfirmedBids { get; set; }
    }
}