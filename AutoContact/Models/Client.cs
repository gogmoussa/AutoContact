using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? DriverLicence { get; set; }

        public DateTime? BirthDate { get; set; }

        public string HashPass { get; set; }

        public string HashSalt { get; set; }

        public uint AddressId { get; set; } // FK

        public uint EmailId { get; set; } // FK


     //   FirstName varchar(50) NOT NULL,
     //   LastName varchar(50) NOT NULL,
     //   DriverLicence varchar(17) NULL,
	 //   BirthDate date NULL,
	 //   AddressId bigint NOT NULL,
     //   EmailId bigint NOT NULL,
     //   HashPass varchar(max) NOT NULL,
     //   HashSalt varchar(max) NOT NULL,
    }
}
