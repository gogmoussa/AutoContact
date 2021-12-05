using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            AccessLevels = new HashSet<AccessLevel>();
        }

        [Key]
        public long ClientId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(17)]
        public string DriverLicence { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        public long AddressId { get; set; }
        [Required]
        public string HashPass { get; set; }
        [Required]
        public string HashSalt { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string PhoneNum { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Clients")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(AccessLevel.Client))]
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
    }
}
