using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;
using TripperAPI.Services;

namespace TripperAPI.Controllers
{
    [ApiController]
    [Route("api/places/{placeId}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<ReviewDto>> GetAllReviewsByPlaceId(int placeId)
        {
            var reviews = await _service.ShowAllReviewsByPlaceId(placeId);
            return reviews;
        }

        [HttpPost]
        public async Task<ActionResult<AddReviewDto>> AddReview(int placeId, [FromBody]AddReviewDto dto)
        {
            await _service.AddReviewToPlaceById(placeId, dto);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            await _service.DeleteReviewByReviewId(reviewId);
            return NoContent();
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult> UpdateReview(int reviewId, [FromBody]UpdateReviewDto dto)
        {
            await _service.EditReviewByReviewId(reviewId, dto);
            return Ok();
        }

    }
}
