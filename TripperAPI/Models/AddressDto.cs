using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models
{
    public class AddressDto
    {
        public string Continent { get; set; }
        public string Country { get; set; }
        #nullable enable
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        #nullable disable
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
