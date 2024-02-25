using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models.ViewModels
{
    public class DetailsCompanies
    {
        public CompaniesDto SelectedCompanies { get; set; }

        public IEnumerable<jobApplicationDto> OpenJobs { get; set; }
    }
}