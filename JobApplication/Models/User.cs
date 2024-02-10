using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPortfolioUrl { get; set; }
        public string AppliedJobs { get; set; }

        public ICollection<jobApplication> jobApplications { get; set; }
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPortfolioUrl { get; set; }
        public string AppliedJobs { get; set; }


    }
}