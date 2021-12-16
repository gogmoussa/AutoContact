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
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public long ClientId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(17)]
        [Display(Name = "Driver's License Number")]
        public string DriverLicence { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        public long AddressId { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNum { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Clients")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(AccessLevel.Client))]
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
        [InverseProperty(nameof(Appointment.Client))]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
