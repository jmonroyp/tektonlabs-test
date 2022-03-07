using Test.Core.Dtos;
using Test.Infraestructure.Entities;
using AutoMapper;

namespace Test.Core.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<ExternalProductDetailDto, ProductDetail>()
                .ForMember(c => c.Rate, k => k.MapFrom(c => c.rating.rate))
                .ForMember(c => c.Count, k => k.MapFrom(c => c.rating.count));
        }
    }
}