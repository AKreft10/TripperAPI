using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models
{
    public class ActualWeather
    {
        public string MainWeather { get; set; }
        public string ImageUrl { get; set; }
        public float Temperature { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
    }
}
