using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Part")]
    public partial class Part
    {
        public Part()
        {
            Invoices = new HashSet<Invoice>();
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        [Key]
        public long PartId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VendorId { get; set; }
        [Column(TypeName = "money")]
        public decimal? CostPrice { get; set; }
        public int ReorderQty { get; set; }
        public int EconomicalOrderQty { get; set; }
        public int? QtyOnHand { get; set; }
        public int? QtyOnOrder { get; set; }
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Parts")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("Parts")]
        public virtual Vendor Vendor { get; set; }
        [InverseProperty(nameof(Invoice.Part))]
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }

        public static explicit operator Part(DbSet<Part> v)
        {
            return (Part)v;
        }
    }
}
