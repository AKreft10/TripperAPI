using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models
{
    public class PhotoDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string Author { get; set; }
        public bool GalleryMember { get; set; }
    }
}
