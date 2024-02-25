using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models.ViewModels
{
    public class UpdatejobApplication
    {
        public jobApplicationDto SelectedApplication { get; set; }


        public IEnumerable<CompaniesDto> CompaniesOptions { get; set; }
    }
}