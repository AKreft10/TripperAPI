using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;
using TripperAPI.Services;

namespace TripperAPI.Controllers
{
    [ApiController]
    [Route("api/places")]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _service;
        private readonly IPlaceDistanceService _placeDistanceService;

        public PlaceController(IPlaceService service, IPlaceDistanceService placeDistanceService)
        {
            _service = service;
            _placeDistanceService = placeDistanceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlaceDto>>> GetAll()
        {
            var places = await _service.GetAll();
            return Ok(places);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PlaceDto>> GetSinglePlace(int id)
        {
            var place = await _service.GetSinglePlaceById(id);
            return Ok(place);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CreatePlaceDto>> CreateNewPlace([FromBody] CreatePlaceDto dto)
        {
            int resultId = await _service.CreateNewPlace(dto);
            return Created($"api/place/{resultId}",null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeletePlaceById(int id)
        {
            await _service.DeleteSinglePlaceById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlace(int id, [FromBody]UpdatePlaceDto dto)
        {
            await _service.UpdateSinglePlaceById(id, dto);
            return Ok();
        }

        [HttpGet("nearby")]
        public async Task<ActionResult<IList<KeyValuePair<Place, TimeAndDistanceDto>>>> GetPlacesWithDistances([FromBody]CoordinatesDto userCoordinates)
        {
            var places = await _placeDistanceService.GetTheNearestPlacesToVisit(userCoordinates);
            return Ok(places);
        }
    }
}
