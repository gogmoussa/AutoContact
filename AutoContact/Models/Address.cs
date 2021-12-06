using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Address")]
    public partial class Address
    {
        public Address()
        {
            Clients = new HashSet<Client>();
            Employees = new HashSet<Employee>();
            Vendors = new HashSet<Vendor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressId { get; set; }
        [Required]
        [StringLength(10)]
        public string StreetNum { get; set; }
        [StringLength(10)]
        public string UnitNum { get; set; }
        [Required]
        [StringLength(50)]
        public string StreetName { get; set; }
        [Required]
        [StringLength(50)]
        public string CityName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProvinceName { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [InverseProperty(nameof(Client.Address))]
        public virtual ICollection<Client> Clients { get; set; }
        [InverseProperty(nameof(Employee.Address))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Vendor.Address))]
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
