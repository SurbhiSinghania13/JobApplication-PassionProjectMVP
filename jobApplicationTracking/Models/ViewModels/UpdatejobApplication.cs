using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models.ViewModels
{
    public class UpdatejobApplication
    {
        public jobApplicationDto SelectedApplication { get; set; }

        // all species to choose from when updating this animal

        public IEnumerable<CompaniesDto> CompaniesOptions { get; set; }
    }
}