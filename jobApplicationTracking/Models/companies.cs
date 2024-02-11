using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models
{
    public class companies
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public string CompanyLocation { get; set; }

        public string CompanyWebsite { get; set; }

        public ICollection<jobApplication> jobApplications { get; set; }
    }
    public class CompaniesDto
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public string CompanyLocation { get; set; }

        public string CompanyWebsite { get; set; }

    }
}