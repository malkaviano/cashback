using AutoMapper;
using Domain.Models;
using Api.Dtos;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ResellerDto, Reseller>();

            CreateMap<SalesPost, Sales>();

            CreateMap<SalesPut, Sales>();

            CreateMap<Sales, Sales>()
                .ForAllMembers(o => o.Condition((source, destination, member) => {
                    return member != null && member.ToString().Trim() != "";
                }));
        }
    }
}