using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Shared.ViewModels
{
    public class CreateOrganisationVM
    {
        //Admin details
        public string UserFullName { get; set; }
        public string UserWorkEmail { get; set; }
        public string Password { get; set; }

        //company details
        public string OrganisationName { get; set; }
        public string LogoUrl { get; set; }
        public string RegistrationNumber { get; set; }
        public string WebAddress { get; set; }
    }
}
