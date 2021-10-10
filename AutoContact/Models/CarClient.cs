using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class CarClient
    {
        public long CarClientId { get; set; }
        public long CarId { get; set; }
        public long ClientId { get; set; }
        public bool IsOwner { get; set; }
    }
}
