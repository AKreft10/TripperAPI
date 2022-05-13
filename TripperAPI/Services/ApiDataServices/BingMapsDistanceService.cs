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
    public class BingMapsDistanceService : IBingMapsDistanceService
    {
        private readonly IConfiguration _configuration;

        public BingMapsDistanceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TimeAndDistanceDto> GetTimeAndDistance(CoordinatesDto userCoords, CoordinatesDto placeCoords)
        {
            string jsonUrl = GetJsonUrl(userCoords, placeCoords);
            var apiDataDeserializedContent = await GetDataFromApiAsync(jsonUrl);
            var resultTimeAndDistance = GetDataFromDeserializedObject(apiDataDeserializedContent);
            return resultTimeAndDistance;
        }

        private static TimeAndDistanceDto GetDataFromDeserializedObject(Rootobject dataContent)
        {
            TimeAndDistanceDto timeAndDistance = new TimeAndDistanceDto()
            {
                Distance = Math.Round((double)dataContent.resourceSets[0].resources[0].results[0].travelDistance),
                Minutes = Math.Round((double)dataContent.resourceSets[0].resources[0].results[0].travelDuration)
            };

            return timeAndDistance;
        }

        private async Task<Rootobject> GetDataFromApiAsync(string jsonUrl)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                response = await client.GetAsync(jsonUrl);
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<Rootobject>(responseContent);

            return deserializedContent;
        }

        private string ReplaceCommasWithDots(double valueToEdit) => valueToEdit.ToString().Replace(',', '.');

        private string GetJsonUrl(CoordinatesDto userCoords, CoordinatesDto placeCoords)
        {
            var apiKey = _configuration.GetValue<string>("BingMapsKey");
            var apiUrl = _configuration.GetValue<string>("BingMapsUrl");

            string lat1 = ReplaceCommasWithDots(placeCoords.Latitude);
            string lon1 = ReplaceCommasWithDots(placeCoords.Longitude);

            string lat2 = ReplaceCommasWithDots(userCoords.Latitude);
            string lon2 = ReplaceCommasWithDots(userCoords.Longitude);

            string jsonUrl = $"{apiUrl}{lat1},{lon1};{lat2},{lon2}&travelMode=driving&key={apiKey}";

            return jsonUrl;
        }

    }
}
