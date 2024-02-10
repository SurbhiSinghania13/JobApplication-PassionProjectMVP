using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobApplication.Models
{
    public class companies
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public string CompanyLocation { get; set; }

        public string CompanyWebsite { get; set; }

        public string JobsAvailable { get; set; }
    }
}