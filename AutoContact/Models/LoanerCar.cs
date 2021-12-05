using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("LoanerCar")]
    public partial class LoanerCar
    {
        public LoanerCar()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public long LoanerCarId { get; set; }
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

        [InverseProperty(nameof(Invoice.LoanerCar))]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
