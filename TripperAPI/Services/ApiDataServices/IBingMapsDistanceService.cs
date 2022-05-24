using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services.ApiDataServices
{
    public interface IBingMapsDistanceService
    {
        Task<IList<KeyValuePair<PlaceDto,TimeAndDistanceDto>>> GetPlacesWithTimeAndDistance(IList<KeyValuePair<PlaceDto, double>> placeList, CoordinatesDto userCoordinates);
    }
}
