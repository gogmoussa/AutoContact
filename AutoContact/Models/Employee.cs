using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AccessLevels = new HashSet<AccessLevel>();
            Departments = new HashSet<Department>();
            InverseManagerNavigation = new HashSet<Employee>();
            Invoices = new HashSet<Invoice>();
            Phones = new HashSet<Phone>();
        }

        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long AddressId { get; set; }
        public string EmployeeSin { get; set; }
        public long? Manager { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string TerminationReason { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }

        public virtual Address Address { get; set; }
        public virtual Employee ManagerNavigation { get; set; }
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> InverseManagerNavigation { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
