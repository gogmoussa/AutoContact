using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Invoice
    {
        public long InvoiceId { get; set; }
        public long EmployeeId { get; set; }
        public long? LoanerCarId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public TimeSpan? HoursWorked { get; set; }
        public long? PartsId { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
