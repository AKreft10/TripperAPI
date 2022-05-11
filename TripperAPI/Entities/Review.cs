using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripperAPI.Entities
{
    public class Review
    {
        public int Id { get; set; }
        #nullable enable
        public string? Content { get; set; }
        #nullable disable
        public int? CreatedById { get; set; }
        [JsonIgnore]
        public virtual User CreatedBy { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public List<Photo> Photos { get; set; }
        [JsonIgnore]
        public int PlaceId { get; set; }
        [JsonIgnore]
        public virtual Place Place { get; set; }
    }
}
