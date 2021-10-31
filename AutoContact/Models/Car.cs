using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Car
    {
        public Car()
        {
            Appointments = new HashSet<Appointment>();
        }

        public long CarId { get; set; }
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public long Odometer { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
