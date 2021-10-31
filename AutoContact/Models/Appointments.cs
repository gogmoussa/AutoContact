using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class Appointments
    {
        public List<Appointment> appointmentList { get; set; }

        public Appointments(List<Appointment> emps)
        {
            this.appointmentList = emps;
        }
    }
}
