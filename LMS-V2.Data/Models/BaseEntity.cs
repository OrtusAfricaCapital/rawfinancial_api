using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Data.Models
{
    public class BaseEntity
    {
        public DateTime CreatedOnUTC { get; set; }
        public DateTime UpdatedOnUTC { get; set; }
    }
}
