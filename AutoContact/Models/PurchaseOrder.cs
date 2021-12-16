using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("PurchaseOrder")]
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Purchase Order ID")]
        public long? PurchaseOrderId { get; set; }
        public long VendorId { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Column("PODate", TypeName = "date")]
        [Display(Name = "Date Ordered")]
        public DateTime PODate { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Date Cancelled")]
        public DateTime? CancelledDate { get; set; }

        [ForeignKey(nameof(VendorId))]
        [InverseProperty("PurchaseOrders")]
        public virtual Vendor Vendor { get; set; }
        [InverseProperty(nameof(PurchaseOrderLineItem.PurchaseOrder))]
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}
