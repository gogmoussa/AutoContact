using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AutoContact.Models
{
    public partial class Employee
    {
        [Key]
        public long EmployeeId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public long AddressId { get; set; }
        public long EmailId { get; set; }
        public string EmployeeSin { get; set; }
        public long? Manager { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string TerminationReason { get; set; }
        [Display(Name = "Password")]
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
    }
}