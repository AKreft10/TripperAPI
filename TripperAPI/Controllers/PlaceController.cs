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
    [Route("api/place")]
    public class PlaceController : ControllerBase   
    {
        private readonly IPlaceService _service;

        public PlaceController(IPlaceService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Place>>> GetAll()
        {
            var places = await _service.GetAll();
            return Ok(places);
        }
    }
}
