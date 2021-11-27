using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime AppointmentStartTime { get; set; }
        public DateTime BookedAtTime { get; set; }
        public string Message { get; set; }
        public long? BookingEmployeeId { get; set; }
        public long ClientId { get; set; }
        public long CarId { get; set; }
        
        public virtual Car Car { get; set; }
        public virtual ICollection<AppointmentInvoice> AppointmentInvoices { get; set; }
    }
}
