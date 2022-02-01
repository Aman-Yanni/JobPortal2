using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal2.Models
{
    public class Applicants
    {
        [Key]
        public int ApplicationId { get; set; }
        public virtual Job jobId { get; set; }
        public virtual ApplicationUser userId { get; set; }

        public DateTime date { get; set; }
        public Status status { get; set; }
    }
    public enum Status
    {
        [Display(Name ="Accepted")]
        Accepted=1,
        [Display(Name = "Pending")]
        Pending=2,
        [Display(Name = "Rejected")]
        Rejected=3
    }
}