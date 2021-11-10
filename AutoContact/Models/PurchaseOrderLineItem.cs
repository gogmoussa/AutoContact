using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class PurchaseOrderLineItem
    {
        public long PurchaseOrderLineItemId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long PartId { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Part Part { get; set; }
    }
}
