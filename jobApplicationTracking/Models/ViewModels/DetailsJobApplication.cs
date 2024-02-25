using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models.ViewModels
{
    public class DetailsJobApplication
    {
        public jobApplicationDto SelectedApplication { get; set; }
        public IEnumerable<UserDto> RegisteredUsers { get; set; }

        public IEnumerable<CompaniesDto> AssociateCompanies { get; set; }


        //public IEnumerable<UserDto> AvailableKeepers { get; set; }
    }
}