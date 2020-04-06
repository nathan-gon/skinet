using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Product, ProductToReturnDto>()
        .ForMember(d => d.ProductBrand, s => s.MapFrom(x => x.ProductBrand.Name))
        .ForMember(d => d.ProductType, s => s.MapFrom(x => x.ProductType.Name))
        .ForMember(d => d.PictureUrl, x => x.MapFrom<ProductUrlResolver>());


    }
  }
}