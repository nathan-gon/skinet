using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrerAggregate;

namespace Core.Interfaces
{
  public interface IOrderService
  {
    Task<Order> CreateOrderAsync(string byerEmail, int deliveryMethod, string basketId,
    Adress ShippingAddress);

    Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);

    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

  }
}