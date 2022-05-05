using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Models
{
    public class PlaceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual List<ReviewDto> Reviews { get; set; }
    }
}
