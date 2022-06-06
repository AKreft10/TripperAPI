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
    public class PlaceService : IPlaceService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PlaceService(DatabaseContext context, IMapper mapper, ILogger<PlaceService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateNewPlace(CreatePlaceDto dto)
        {

            var place = _mapper.Map<Place>(dto);
            await _context.AddAsync(place);
            await _context.SaveChangesAsync();


            _logger.LogInformation($"Created new place, ID: {place.Id}, NAME: {place.Name}");
            return place.Id;
        }

        public async Task DeleteSinglePlaceById(int id)
        {
            _logger.LogInformation($"Place with id: {id} DELETE method invoked.");


            var place = await _context
                .Places
                .FirstOrDefaultAsync(x => x.Id == id);

            if(place is null)
            {
                throw new NotFound("Place not found.. ):");
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<PlaceDto>> GetAll(QueryParameters query)
        {
            var baseQuery = _context
                .Places
                .Include(a => a.Address)
                .Include(r => r.Reviews)
                .ThenInclude(p => p.Photos)
                .Where(z => query.searchPhrase == null || (z.Name.ToLower().Contains(query.searchPhrase.ToLower()) || z.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            var places = await baseQuery
                .Skip(query.pageSize*(query.pageNumber-1))
                .Take(query.pageSize)
                .ToListAsync();


            if(places is null)
            {
                throw new NotFound("No places here ):");
            }

            var result = _mapper.Map<List<PlaceDto>>(places);
            var pagedResult = new PagedResult<PlaceDto>(result,baseQuery.Count(),query.pageSize, query.pageNumber);

            return pagedResult;
        }

        public async Task<PlaceDto> GetSinglePlaceById(int id)
        {
            var place = await _context
                .Places
                .Include(a => a.Address)
                .Include(r => r.Reviews)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(i => i.Id == id);

            if(place is null)
            {
                throw new NotFound("No place here ):");
            }

            var result = _mapper.Map<PlaceDto>(place);

            return result;
        }

        public async Task UpdateSinglePlaceById(int id, UpdatePlaceDto dto)
        {
            _logger.LogInformation($"Place with id: {id} UPDATE method invoked");

            var place = await _context
                .Places
                .Include(a => a.Address)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place is null)
                throw new NotFound("Place not found, nothing to edit here ):");

            place.Name = dto.Name;
            place.Description = dto.Description;

            place.Address.Continent = dto.Continent;
            place.Address.Country = dto.Country;
            place.Address.City = dto.City;
            place.Address.PostalCode = dto.PostalCode;
            place.Address.Street = dto.Street;
            place.Address.Latitude = dto.Latitude;
            place.Address.Longitude = dto.Longitude;

            await _context.SaveChangesAsync();
        }
    }
}