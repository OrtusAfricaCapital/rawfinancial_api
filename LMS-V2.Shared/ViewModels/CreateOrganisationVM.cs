using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS_V2.Shared.ViewModels
{
    public class CreateOrganisationVM
    {
        //Admin details
        [Required]
        public string UserFullName { get; set; }
        [Required]
        public string UserWorkEmail { get; set; }
        [Required]
        public string Password { get; set; }

        //company details
        [Required]
        public string OrganisationName { get; set; }
    }
}
