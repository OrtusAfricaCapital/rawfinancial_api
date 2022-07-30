using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Data.Models
{
    public class OrganisationsStaff : BaseEntity
    {
        public int Id { get; set; }
        public int OrganisationId { get; set; }
        public int StaffId { get; set; }

        public virtual Organisation Organisation { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
