using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models
{
    public class QueryParameters
    {
        public string searchPhrase { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
