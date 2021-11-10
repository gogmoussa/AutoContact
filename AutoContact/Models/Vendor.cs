using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Parts = new HashSet<Part>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public long VendorId { get; set; }
        public string Name { get; set; }
        public long? AddressId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MainContact { get; set; }
        public string Type { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
