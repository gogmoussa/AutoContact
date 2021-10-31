using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Phone
    {
        public long PhoneNumId { get; set; }
        public long? ClientId { get; set; }
        public long? EmployeeId { get; set; }
        public string PhoneNum { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
