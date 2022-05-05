using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Services
{
    public interface IPlaceService
    {
        Task<List<Place>> GetAll();
    }
}
