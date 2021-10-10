using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class AccessLevel
    {
        public long AccessLevelId { get; set; }
        public long? ClientId { get; set; }
        public long? EmployeeId { get; set; }
        public string AccessLevel1 { get; set; }
    }
}
