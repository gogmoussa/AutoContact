using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Keyless]
    [Table("CarClient")]
    public partial class CarClient
    {
        public long CarClientId { get; set; }
        public long CarId { get; set; }
        public long ClientId { get; set; }
        public bool IsOwner { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}
