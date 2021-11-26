using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AutoContact.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseOrderId { get; }
        public long VendorId { get; set; }
        public double? Amount { get; set; }
        public DateTime PODate { get; set; }
        public DateTime? CancelledDate { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }

    }
}
