using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public interface IWeatherService
    {
        Task<ActualWeather> GetWeatherByPlaceId(int id);
    }
}
