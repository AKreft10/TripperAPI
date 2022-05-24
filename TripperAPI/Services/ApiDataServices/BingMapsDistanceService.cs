using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services.ApiDataServices
{
    public class BingMapsDistanceService : ApiDataService, IBingMapsDistanceService
    {
        private readonly IConfiguration _configuration;

        public BingMapsDistanceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetJsonUrl(CoordinatesDto userCoords, IList<KeyValuePair<PlaceDto, double>> placeList)
        {
            var apiKey = _configuration.GetValue<string>("BingMapsKey");
            var apiUrl = _configuration.GetValue<string>("BingMapsUrl");
            int iterationCount = 0;

            string jsonUrl = $"{apiUrl}";

            foreach(var place in placeList)
            {
                iterationCount++;
                jsonUrl += $"{ReplaceCommasWithDots(place.Key.Address.Latitude)},{ReplaceCommasWithDots(place.Key.Address.Longitude)}";

                if (iterationCount != placeList.Count)
                    jsonUrl += ";";
            }

            jsonUrl += $"&destinations={ReplaceCommasWithDots(userCoords.Latitude)},{ReplaceCommasWithDots(userCoords.Longitude)}&travelMode=driving&key={apiKey}";

            return jsonUrl;
        }

        public async Task<IList<KeyValuePair<PlaceDto, TimeAndDistanceDto>>> GetPlacesWithTimeAndDistance(IList<KeyValuePair<PlaceDto, double>> placeList, CoordinatesDto userCoordinates)
        {
            var jsonUrl = GetJsonUrl(userCoordinates, placeList);
            var apiDataObject = await GetDataFromApiAsync<Rootobject>(jsonUrl);

            List<KeyValuePair<PlaceDto, TimeAndDistanceDto>> placesWithTimeAndDistance = new List<KeyValuePair<PlaceDto, TimeAndDistanceDto>>();

            for (int i = 0; i < placeList.Count; i++)
            {
                var timeAndDistance = new TimeAndDistanceDto()
                {
                    Distance = Math.Round((double)apiDataObject.resourceSets[0].resources[0].results[i].travelDistance),
                    Minutes = Math.Round((double)apiDataObject.resourceSets[0].resources[0].results[i].travelDuration)
                };
                var placeWithTimeAndDistance = new KeyValuePair<PlaceDto, TimeAndDistanceDto>(placeList[i].Key, timeAndDistance);
                placesWithTimeAndDistance.Add(placeWithTimeAndDistance);
            }

            return placesWithTimeAndDistance;
        }
    }
}
