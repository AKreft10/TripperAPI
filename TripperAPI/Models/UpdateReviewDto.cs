using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Models
{
    public class UpdateReviewDto
    {
        [MaxLength(400)]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Rating { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
