using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;
using TripperAPI.Services.ApiDataServices;

namespace TripperAPI.Services
{
    public class PlaceDistanceService : IPlaceDistanceService
    {
        private readonly DatabaseContext _context;
        private readonly IBingMapsDistanceService _bingMapsService;
        private readonly IMapper _mapper;

        public PlaceDistanceService(DatabaseContext context, IBingMapsDistanceService bingMapsService, IMapper mapper)
        {
            _context = context;
            _bingMapsService = bingMapsService;
            _mapper = mapper;
        }

        public async Task<IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>>> GetTheNearestPlacesToVisit(CoordinatesDto userCoordinates)
        {
            var places = await GetTenClosestPlacesFromDatabase(userCoordinates);
            var timeAndDistanceList = await GetTimeAndDistanceListBetweenUserAndPlaces(places, userCoordinates);
            var sortedResult = SortPlacesByDistance(timeAndDistanceList);
            return sortedResult;
        }

        private IList<KeyValuePair<PlaceDto,TimeAndDistanceDto>> SortPlacesByDistance(IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>> unorderedList)
        {
            IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>> orderedList = unorderedList
                .OrderBy(x => x.Value.Minutes)
                .Take(10)
                .ToList();

            return orderedList;
        }

        private async Task<IList<KeyValuePair<PlaceDto, double>>> GetTenClosestPlacesFromDatabase(CoordinatesDto userCoordinates)
        {
            var allPlaces = await _context.Places
                .Include(a => a.Address)
                .ToListAsync();

            var allPlacesDto = _mapper.Map<List<PlaceDto>>(allPlaces);

            List<KeyValuePair<PlaceDto, double>> placesWithDistances = new List<KeyValuePair<PlaceDto, double>>();

                foreach(var place in allPlacesDto)
                {
                    var destinationCoordinates = new CoordinatesDto()
                    {
                        Latitude = place.Address.Latitude,
                        Longitude = place.Address.Longitude
                    };

                    var distance = GetDistanceBetweenTwoPointsByCoordinates(userCoordinates, destinationCoordinates);
                    var placeWithDistance = new KeyValuePair<PlaceDto, double>(place, distance);
                    placesWithDistances.Add(placeWithDistance);
                };

            return placesWithDistances;
        }

        private static double GetDistanceBetweenTwoPointsByCoordinates(CoordinatesDto point1, CoordinatesDto point2)
        {
            var d1 = point1.Latitude * (Math.PI / 180.0);
            var num1 = point1.Longitude * (Math.PI / 180.0);
            var d2 = point2.Latitude * (Math.PI / 180.0);
            var num2 = point2.Longitude * (Math.PI / 180.0) - num1;

            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
        private async Task<IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>>> GetTimeAndDistanceListBetweenUserAndPlaces(IList<KeyValuePair<PlaceDto, double>> placeList, CoordinatesDto userCoordinates)
        {
            var timeAndDistanceList = await _bingMapsService.GetPlacesWithTimeAndDistance(placeList, userCoordinates);
            return timeAndDistanceList;
        }
    }
}
