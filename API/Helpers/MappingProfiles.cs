using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

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

      //여기서 리버스맵을 쓰는 이유는 어드레스를 업데이트할때 그냥 디티오만 업데이트하는것이아니라 
      //어드레스디티오를 클라리언트로부터 받아서 다시 어드레스로 넣어준다음에 다시 디티오로 리턴해야 하기 때문에
      CreateMap<Adress, AdressDto>().ReverseMap();

      CreateMap<CustomerBasketDto, CustomerBasket>();

      CreateMap<BasketItemDto, BasketItem>();



    }
  }
}