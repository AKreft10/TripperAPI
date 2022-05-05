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
    }
}