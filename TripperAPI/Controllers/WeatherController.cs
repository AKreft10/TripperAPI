using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Services;

namespace TripperAPI.Controllers
{
    [ApiController]
    [Route("api/places/{placeId}/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IWeatherService _weatherService;

        public WeatherController(DatabaseContext context, IWeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWeatherByPlaceId(int placeId)
        {
            var result = await _weatherService.GetWeatherByPlaceId(placeId);
            return Ok(result);
        }


    }
}
