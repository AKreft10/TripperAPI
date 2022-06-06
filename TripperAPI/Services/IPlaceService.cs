using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public interface IPlaceService
    {
        Task<PagedResult<PlaceDto>> GetAll(QueryParameters query);
        Task<PlaceDto> GetSinglePlaceById(int id);
        Task<int> CreateNewPlace(CreatePlaceDto dto);
        Task DeleteSinglePlaceById(int id);
        Task UpdateSinglePlaceById(int id, UpdatePlaceDto dto);
    }
}
