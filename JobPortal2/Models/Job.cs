using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal2.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public string Desc { get; set; } 

        public decimal Salary { get; set; }
        [Range(0, 30)]
        public int Experience { get; set; }

        public Degree degree { get; set; }

        public JobCategory Category { get; set; }

        public virtual ApplicationUser UserID { get; set; }

        public bool Active { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }

    }
    

    public enum JobCategory
    {
        [Display(Name = "Sales")]
        Sales = 1,
        [Display(Name = "InformationTechnology")]
        IT = 2,
        [Display(Name = "Banking")]
        Banking = 3,
        [Display(Name = "Construction")]
        Construction = 4,
        [Display(Name = "Retail")]
        Retail = 5,
        [Display(Name = "Marketing")]
        Marketing = 6
    }
    public enum Degree
    {
        [Display(Name = "Undergarduate")]
        UnderGrad = 1,
        [Display(Name = "Bachelor")]
        Bachelor = 2,
        [Display(Name = "Masters")]
        Masters = 3,
        [Display(Name = "Phd")]
        Phd = 4
    }
}