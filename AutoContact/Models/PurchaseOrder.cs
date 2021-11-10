﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        public long PurchaseOrderId { get; set; }
        public long VendorId { get; set; }
        public double Amount { get; set; }
        public DateTime PODate { get; set; }
        public DateTime? CancelledDate { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}