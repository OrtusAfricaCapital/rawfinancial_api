using LMS_V2.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS_V2.Data.Models
{
    public class Staff : BaseEntity
    {
        [Key]
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } 
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<OrganisationsStaff> OrganisationsStaff { get; set; }
    }
}
