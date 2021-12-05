using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Car")]
    public partial class Car
    {
        public Car()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public long CarId { get; set; }
        [Required]
        [Column("VIN")]
        [StringLength(17)]
        public string Vin { get; set; }
        [Required]
        [StringLength(50)]
        public string Make { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        [Required]
        [StringLength(50)]
        public string Colour { get; set; }
        public long Odometer { get; set; }

        [InverseProperty(nameof(Appointment.Car))]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
