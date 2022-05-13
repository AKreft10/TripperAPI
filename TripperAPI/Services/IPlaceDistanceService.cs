using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public interface IPlaceDistanceService
    {
        Task<IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>>> GetTheNearestPlacesToVisit(CoordinatesDto userCoordinates);
    }
}
