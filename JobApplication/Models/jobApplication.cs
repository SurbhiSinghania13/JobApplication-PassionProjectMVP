using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobApplication.Models
{
    public class jobApplication
    {
        [Key]
        public int JobApplicationID { get; set; }
        public string JobTitle { get; set; }

        //weight is in kg
        public string CompanyName { get; set; }

        public string Applicants { get; set; }


        //An animal belongs to one species
        //A species can have many animals
        [ForeignKey("companies")]
        public int CompanyId { get; set; }
        public virtual companies companies { get; set; }


        //an animal can be taken care of by many keepers
        public ICollection<User> Users { get; set; }
    }
}