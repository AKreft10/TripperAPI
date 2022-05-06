using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ReviewService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddReviewToPlaceById(int placeId, AddReviewDto dto)
        {
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
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (review is null)
                throw new NotFound("No review here.. ):");

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public async Task EditReviewByReviewId(int reviewId, UpdateReviewDto dto)
        {
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
