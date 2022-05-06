using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Models
{
    public class AddReviewDto
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
