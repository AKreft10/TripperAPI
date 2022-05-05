using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public bool GalleryMember { get; set; }
    }
}
