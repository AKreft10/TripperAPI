using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly DatabaseContext _context;

        public PlaceService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Place>> GetAll()
        {
            var places = await _context
                .Places
                .Include(a => a.Address)
                .Include(r => r.Reviews)
                .ThenInclude(p => p.Photos)
                .ToListAsync();

            return places;
        }
    }
}