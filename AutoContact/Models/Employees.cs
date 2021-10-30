using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class Employees
    {
        public List<Employee> EmployeeList { get; set; }

        public Employees(List<Employee> emps)
        {
            this.EmployeeList = emps;
        }
    }
}
