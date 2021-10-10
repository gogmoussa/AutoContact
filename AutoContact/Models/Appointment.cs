using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Appointment
    {
        public long AppointmentId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime BookedAtTime { get; set; }
        public long BookingEmployeeId { get; set; }
        public long? InvoiceId { get; set; }
        public long CarId { get; set; }
    }
}
