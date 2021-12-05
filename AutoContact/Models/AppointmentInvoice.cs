using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("AppointmentInvoice")]
    public partial class AppointmentInvoice
    {
        [Key]
        public long AppointmentInvoiceId { get; set; }
        public long AppointmentId { get; set; }
        public long InvoiceId { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("AppointmentInvoices")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty("AppointmentInvoices")]
        public virtual Invoice Invoice { get; set; }
    }
}
