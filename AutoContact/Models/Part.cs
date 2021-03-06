using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "Part ID")]
        public long PartId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VendorId { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Cost Price")]
        public decimal? CostPrice { get; set; }
        [Display(Name = "Reorder Qty")]
        public int ReorderQty { get; set; }
        [Display(Name = "Economical Order Qty")]
        public int EconomicalOrderQty { get; set; }
        [Display(Name = "Qty on Hand")]
        public int? QtyOnHand { get; set; }
        [Display(Name = "Qty on Order")]
        public int? QtyOnOrder { get; set; }
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        [NotMapped]
        public List<SelectListItem> AllCategories { get; set; }
        [NotMapped]
        public List<SelectListItem> AllVendors { get; set; }

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
