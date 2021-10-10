using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Client
    {
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverLicence { get; set; }
        public DateTime? BirthDate { get; set; }
        public long AddressId { get; set; }
        public long EmailId { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
    }
}
