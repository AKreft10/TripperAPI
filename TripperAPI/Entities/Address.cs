﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripperAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        #nullable enable
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        #nullable disable
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonIgnore]
        public virtual Place Place { get; set; }
    }
}
