using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ReviewService(DatabaseContext context, IMapper mapper, ILogger<ReviewService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddReviewToPlaceById(int placeId, AddReviewDto dto)
        {
            _logger.LogInformation($"Review with id: {placeId} ADD method invoked.");


            await _context.Reviews.AddAsync(new Review
            {
                Content = dto.Content,
                Author = dto.Author,
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
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewByReviewId(int reviewId)
        {
            _logger.LogInformation($"Review with id: {reviewId} DELETE method invoked.");

            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (review is null)
                throw new NotFound("No review here.. ):");

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public async Task EditReviewByReviewId(int reviewId, UpdateReviewDto dto)
        {
            _logger.LogInformation($"Review with id: {reviewId} UPDATE method invoked.");

            var review = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == reviewId);

            if(review is null)
            {
                throw new NotFound("Review not found.. ):");
            }

            var photos = await
                _context
                .Photos
                .Where(x => x.ReviewId == review.Id)
                .ToListAsync();


            review.Content = dto.Content;
            review.Author = dto.Author;
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
