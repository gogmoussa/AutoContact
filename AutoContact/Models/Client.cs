using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AutoContact.Models
{
    public partial class Client
    {
        public Client()
        {
            AccessLevels = new HashSet<AccessLevel>();
        }

        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverLicence { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public long AddressId { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
    }
}
