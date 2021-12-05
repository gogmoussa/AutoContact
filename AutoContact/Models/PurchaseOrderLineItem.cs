using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("PurchaseOrderLineItem")]
    public partial class PurchaseOrderLineItem
    {
        [Key]
        public long PurchaseOrderLineItemId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long PartId { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(PartId))]
        public virtual Part Part { get; set; }
        [ForeignKey(nameof(PurchaseOrderId))]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
