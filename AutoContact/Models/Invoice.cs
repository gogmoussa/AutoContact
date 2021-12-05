using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Invoice")]
    public partial class Invoice
    {
        public Invoice()
        {
            AppointmentInvoices = new HashSet<AppointmentInvoice>();
        }

        [Key]
        public long InvoiceId { get; set; }
        public long EmployeeId { get; set; }
        public long? LoanerCarId { get; set; }
        public long? PartId { get; set; }
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public double? HoursWorked { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CancelledDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CompletedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PaidDate { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Invoices")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(LoanerCarId))]
        [InverseProperty("Invoices")]
        public virtual LoanerCar LoanerCar { get; set; }
        [ForeignKey(nameof(PartId))]
        [InverseProperty("Invoices")]
        public virtual Part Part { get; set; }
        [InverseProperty(nameof(AppointmentInvoice.Invoice))]
        public virtual ICollection<AppointmentInvoice> AppointmentInvoices { get; set; }
    }
}
