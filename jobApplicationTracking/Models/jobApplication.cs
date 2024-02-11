using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models
{
    public class jobApplication
    {
        [Key]
        public int JobApplicationID { get; set; }
        public string JobTitle { get; set; }

        //weight is in kg
        public string CompanyName { get; set; }

        public string JobLocation { get; set;}

        public ICollection<User> users { get; set; }

        [ForeignKey("companies")]
        public int CompanyID { get; set; }
        public virtual companies companies { get; set; }
    }
    public class jobApplicationDto
    {
        public int JobApplicationID { get; set; }
        public string JobTitle { get; set; }

        //weight is in kg
        public string CompanyName { get; set; }

        public string JobLocation { get; set; }
        public int CompanyID { get; set; }
        public string Industry { get; set; }



    }
}