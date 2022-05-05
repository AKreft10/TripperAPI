using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;

namespace TripperAPI.MappingProfiles
{
    public class CreatePlaceMappingProfile : Profile
    {
        public CreatePlaceMappingProfile()
        {
            CreateMap<CreatePlaceDto, Place>()
                .ForMember(p => p.Address,
                    c => c.MapFrom(dto => new Address()
                    {
                        Continent = dto.Continent,
                        Country = dto.Country,
                        City = dto.City,
                        Street = dto.Street,
                        PostalCode = dto.PostalCode,
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,
                    }));
        }
    }
}
