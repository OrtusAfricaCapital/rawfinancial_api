using LMS_V2.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS_V2.Data.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public StaffRole StaffRole { get; set; }
        [Required]
        public string Email { get; set; } //unique
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}
