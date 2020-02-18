using AutoMapper;
using Domain.Models;
using Api.Dtos;

namespace Api.Helpers {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles()
        {
            CreateMap<ResellerDto, Reseller>()
                .ReverseMap();
        }
    }
}