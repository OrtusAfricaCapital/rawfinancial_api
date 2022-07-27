using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS_V2.Data.Models
{
    public class Organisation
    {
        [Key]
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string WebAddress { get; set; }
        public string LogoUrl { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}
