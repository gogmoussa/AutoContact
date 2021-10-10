using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Department
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long EmployeeId { get; set; }
    }
}
