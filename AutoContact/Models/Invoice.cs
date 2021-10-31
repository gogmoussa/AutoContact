using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            AppointmentInvoices = new HashSet<AppointmentInvoice>();
        }

        public long InvoiceId { get; set; }
        public long EmployeeId { get; set; }
        public long? LoanerCarId { get; set; }
        public long? PartsId { get; set; }
        public decimal Cost { get; set; }
        public double? HoursWorked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? PaidDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LoanerCar LoanerCar { get; set; }
        public virtual ICollection<AppointmentInvoice> AppointmentInvoices { get; set; }
    }
}
