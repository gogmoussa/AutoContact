using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        public Employee()
        {
            AccessLevels = new HashSet<AccessLevel>();
            Departments = new HashSet<Department>();
            InverseManagerNavigation = new HashSet<Employee>();
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public long AddressId { get; set; }
        [Required]
        [Column("EmployeeSIN")]
        [Display(Name = "Employee SIN")]
        [StringLength(9)]
        public string EmployeeSin { get; set; }
        public long? Manager { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Termination Date")]
        public DateTime? TerminationDate { get; set; }
        [StringLength(10)]
        [Display(Name = "Termination Reason")]
        public string TerminationReason { get; set; }
        public string HashPass { get; set; }
        public string HashSalt { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNum { get; set; }
        [NotMapped]
        public List<SelectListItem> AllEmployees { get; set; }
        [NotMapped]
        public List<SelectListItem> AllAccessLevels
        {
            get
            {
                return new List<SelectListItem>() { new SelectListItem("Admin", "Admin"), new SelectListItem("Employee", "Employee"), new SelectListItem("Client", "Client") };
            }
        }
        [NotMapped]
        public AccessLevel EmployeeAccessLevel { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Employees")]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(Manager))]
        [InverseProperty(nameof(Employee.InverseManagerNavigation))]
        public virtual Employee ManagerNavigation { get; set; }
        [InverseProperty(nameof(AccessLevel.Employee))]
        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
        [InverseProperty(nameof(Department.Employee))]
        public virtual ICollection<Department> Departments { get; set; }
        [InverseProperty(nameof(Employee.ManagerNavigation))]
        public virtual ICollection<Employee> InverseManagerNavigation { get; set; }
        [InverseProperty(nameof(Invoice.Employee))]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
