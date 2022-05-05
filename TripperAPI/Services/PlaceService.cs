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
    public class PlaceService : IPlaceService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public PlaceService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateNewPlace(CreatePlaceDto dto)
        {
            var place = _mapper.Map<Place>(dto);
            await _context.AddAsync(place);
            await _context.SaveChangesAsync();

            return place.Id;
        }

        public async Task DeleteSinglePlaceById(int id)
        {
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

        public async Task<List<PlaceDto>> GetAll()
        {
            var places = await _context
                .Places
                .Include(a => a.Address)
                .Include(r => r.Reviews)
                .ThenInclude(p => p.Photos)
                .ToListAsync();


            if(places is null)
            {
                throw new NotFound("No places here ):");
            }

            var result = _mapper.Map<List<PlaceDto>>(places);

            return result;
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
    }
}