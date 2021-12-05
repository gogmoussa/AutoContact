using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Vendor")]
    public partial class Vendor
    {
        public Vendor()
        {
            Parts = new HashSet<Part>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        [Key]
        public long VendorId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public long? AddressId { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string MainContact { get; set; }
        [StringLength(50)]
        public string Type { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Vendors")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(Part.Vendor))]
        public virtual ICollection<Part> Parts { get; set; }
        [InverseProperty(nameof(PurchaseOrder.Vendor))]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
