using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkWarriors.Models
{
    public class CompletedPath
    {
        public int CompletedPathId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int CompletedBidsID { get; set; }
        [ForeignKey("CompletedBidsID")]
        public virtual CompletedBids CompletedBids { get; set; }
    }
}