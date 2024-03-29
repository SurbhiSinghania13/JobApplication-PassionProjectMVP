﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobApplicationTracking.Models.ViewModels
{
    public class DetailsUser
    {
        public UserDto SelectedUser { get; set; }
        public IEnumerable<jobApplicationDto> AppliedJobs { get; set; }

        public IEnumerable<jobApplicationDto> AvailableJobs { get; set; }
    }
}