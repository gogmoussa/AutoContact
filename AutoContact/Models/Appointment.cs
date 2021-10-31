using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentInvoices = new HashSet<AppointmentInvoice>();
        }

        public long AppointmentId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime BookedAtTime { get; set; }
        public long BookingEmployeeId { get; set; }
        public long? InvoiceId { get; set; }
        public long CarId { get; set; }

        public virtual Car Car { get; set; }
        public virtual ICollection<AppointmentInvoice> AppointmentInvoices { get; set; }
    }
}
