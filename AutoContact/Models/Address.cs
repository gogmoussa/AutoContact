using System;
using System.Collections.Generic;

#nullable disable

namespace AutoContact.Models
{
    public partial class Address
    {
        public long AddressId { get; set; }
        public string StreetNum { get; set; }
        public string UnitNum { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public string Country { get; set; }
    }
}
