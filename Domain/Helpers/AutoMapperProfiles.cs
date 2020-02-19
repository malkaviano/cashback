using AutoMapper;
using Domain.Models;
using System;
using Domain.Values;

// Jon Skeet self contained solution for use inside libs
public static class Mapping
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg => {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Sales, SalesResult>().ForMember(
            dest => dest.Cpf,
            opt => opt.MapFrom(src => src.Reseller.Cpf)
        );
    }
}