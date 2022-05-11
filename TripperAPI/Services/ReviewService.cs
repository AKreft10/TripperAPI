using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Authorization;
using TripperAPI.Entities;
using TripperAPI.Middleware.Exceptions;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ReviewService(DatabaseContext context, IMapper mapper, ILogger<ReviewService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task AddReviewToPlaceById(int placeId, AddReviewDto dto)
        {
            await _context.Reviews.AddAsync(new Review
            {
                Content = dto.Content,
                CreatedById = _userContextService.GetUserId,
                Rating = dto.Rating,
                Created = DateTime.Now,
                PlaceId = placeId,
                Photos = dto.Photos.Select(y => new Photo()
                {
                    Url = y.Url,
                    Author = dto.Author,
                    GalleryMember = false
                })
                .ToList()
            });;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"New review for place with id: {placeId} has been added by user with id: {_userContextService.GetUserId}");
        }

        public async Task DeleteReviewByReviewId(int reviewId)
        {

            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (review is null)
                throw new NotFound("No review here.. ):");

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            _logger.LogInformation($"Review with id: {reviewId} has been deleted by user with id: {_userContextService.GetUserId}");
        }

        public async Task EditReviewByReviewId(int reviewId, UpdateReviewDto dto)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == reviewId);

            if(review is null)
            {
                throw new NotFound("Review not found.. ):");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, review, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new Forbidden("Unauthorized.. ):");
            }

            var photos = await
                _context
                .Photos
                .Where(x => x.ReviewId == review.Id)
                .ToListAsync();


            review.Content = dto.Content;
            review.Rating = dto.Rating;
            review.Photos.RemoveAll(r => r.ReviewId == reviewId);

            review.Photos = dto.Photos.Select(y => new Photo()
            {
                Url = y.Url,
                Author = dto.Author,
                GalleryMember = false
            })
            .ToList();

            _context.SaveChanges();

            _logger.LogInformation($"Review with id: {reviewId} has been updated by user with id: {_userContextService.GetUserId}");
        }

        public async Task<List<ReviewDto>> ShowAllReviewsByPlaceId(int placeId)
        {
            var place = await GetSinglePlaceById(placeId);

            if (place is null)
                throw new NotFound("This place doesn't exist in our database.. ):");

            if (!place.Reviews.Any())
                throw new NotFound("No reviews for this place yet.. ):");

            var result = _mapper.Map<List<ReviewDto>>(place.Reviews);

            return result;
        }

        private async Task<Place> GetSinglePlaceById(int id)
        {
            var place = await
                _context
                .Places
                .Include(r => r.Reviews)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place is null)
                throw new NotFound("Place not found.. ):");

            return place;
        }
    }
}
