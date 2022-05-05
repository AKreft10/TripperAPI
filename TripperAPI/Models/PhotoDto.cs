using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models
{
    public class PhotoDto
    {
        public string Url { get; set; }
        public string Author { get; set; }
        public bool GalleryMember { get; set; }
    }
}
