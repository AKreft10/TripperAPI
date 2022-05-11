using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> ShowAllReviewsByPlaceId(int placeId);
        Task AddReviewToPlaceById(int placeId, AddReviewDto dto);
        Task EditReviewByReviewId(int reviewId, UpdateReviewDto dto);
        Task DeleteReviewByReviewId(int reviewId);
    }
}
