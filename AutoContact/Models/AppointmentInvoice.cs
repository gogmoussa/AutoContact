using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class AppointmentInvoice
    {
        public long AppointmentInvoiceId { get; set; }
        public long AppointmentId { get; set; }
        public long InvoiceId { get; set; }
    }
}
