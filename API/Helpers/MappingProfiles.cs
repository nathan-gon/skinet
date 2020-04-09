using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrerAggregate;

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
      CreateMap<Core.Entities.Identity.Adress, AdressDto>().ReverseMap();

      CreateMap<CustomerBasketDto, CustomerBasket>();

      CreateMap<BasketItemDto, BasketItem>();
      //여기서 다시한번 어드레스를 맵핑해주는데 위에 어드레스랑 밑에 어드레스가 
      //다른 엔티티이기 때문 위에는 아이덴티티 밑에는 어그리것 어드레스
      CreateMap<AdressDto, Core.Entities.OrerAggregate.Adress>();

      CreateMap<Order, OrderToReturnDto>()
        .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
        .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));


      CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
        .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
        .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
        .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());


    }
  }
}