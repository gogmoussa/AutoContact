using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class Part
    {
        public Part()
        {
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        public long PartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VendorId { get; set; }
        public double? CostPrice { get; set; }
        public int ReorderQty { get; set; }
        public int EconomicalOrderQty { get; set; }
        public int? QtyOnHand { get; set; }
        public int? QtyOnOrder { get; set; }
        public long CategoryId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}
