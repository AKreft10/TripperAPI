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
    public class OpenWeatherService : ApiDataService, IOpenWeatherService
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActualWeather> GetWeatherByPlaceCoordinates(CoordinatesDto coordinates)
        {
            var jsonUrl = GetJsonUrl(coordinates);
            var apiResponse = await GetDataFromApiAsync<WeatherObject>(jsonUrl);

            ActualWeather timeAndWeatherDto = new ActualWeather()
            {
                MainWeather = apiResponse.current.condition.text,
                ImageUrl = apiResponse.current.condition.icon,
                Temperature = apiResponse.current.temp_c,
                WindSpeed = Math.Round(apiResponse.current.wind_kph,2),
                Humidity = apiResponse.current.humidity
            };

            return timeAndWeatherDto;
        }

        private string GetJsonUrl(CoordinatesDto coords)
        {
            var apiKey = _configuration.GetValue<string>("WeatherApiKey");
            var apiUrl = _configuration.GetValue<string>("WeatherApiUrl");

            var json = $"{apiUrl}key={apiKey}&q={ReplaceCommasWithDots(coords.Latitude)},{ReplaceCommasWithDots(coords.Longitude)}&aqi=no";

            return json;
        }
    }

}
