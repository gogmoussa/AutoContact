using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Appointment")]
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentInvoices = new HashSet<AppointmentInvoice>();
        }

        [Key]
        public long AppointmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppointmentStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BookedAtTime { get; set; }
        public string Message { get; set; }
        public long? BookingEmployeeId { get; set; }
        public long ClientId { get; set; }
        public long CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        [InverseProperty("Appointments")]
        public virtual Car Car { get; set; }
        [ForeignKey(nameof(ClientId))]
        [InverseProperty("Appointments")]
        public virtual Client Client { get; set; }
        [ForeignKey(nameof(BookingEmployeeId))]
        [InverseProperty("Appointments")]
        public virtual Employee Employee { get; set; }
        [InverseProperty(nameof(AppointmentInvoice.Appointment))]
        public virtual ICollection<AppointmentInvoice> AppointmentInvoices { get; set; }
    }
}
