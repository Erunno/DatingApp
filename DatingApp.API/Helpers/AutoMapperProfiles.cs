using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.PhotoUrl,
                            opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)))
                .ForMember(dest => dest.Age,
                            opt => opt.MapFrom(src => src.DateOfBirth.GetAge()));
            
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl,
                            opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)))
                .ForMember(dest => dest.Age,
                            opt => opt.MapFrom(src => src.DateOfBirth.GetAge()));
            
            CreateMap<Photo, PhotoForDetailDto>();
        }
    }
}
