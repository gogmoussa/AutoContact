using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Client
    {
        public Client()
        {
            AccessLevels = new HashSet<AccessLevel>();
            Phones = new HashSet<Phone>();
        }

        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverLicence { get; set; }
        public DateTime? BirthDate { get; set; }
        public long AddressId { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
        public string Email { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
