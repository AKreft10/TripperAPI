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
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherService _service;
        private readonly DatabaseContext _context;

        public WeatherService(IOpenWeatherService service, DatabaseContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<ActualWeather> GetWeatherByPlaceId(int id)
        {
            var place = await _context.Places
                .Include(a => a.Address)
                .FirstOrDefaultAsync(p => p.Id == id);

            var placeCoordinates = new CoordinatesDto()
            {
                Latitude = place.Address.Latitude,
                Longitude = place.Address.Longitude
            };

            var actualWeather = await _service.GetWeatherByPlaceCoordinates(placeCoordinates);
            return actualWeather;
        }
    }
}
