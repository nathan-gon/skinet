using System;
using System.Linq.Expressions;
using Core.Entities.OrerAggregate;

namespace Core.Specifications
{
  public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
  {
    public OrdersWithItemsAndOrderingSpecification(string email) : base(x => x.BuyerEmail == email)
    {
      AddInclude(x => x.OrderItems);
      AddInclude(x => x.DeliveryMethod);
      AddOrderByDescending(x => x.OrderDate);
    }

    public OrdersWithItemsAndOrderingSpecification(int id, string email)
    : base(x => x.Id == id && x.BuyerEmail == email)
    {
      AddInclude(x => x.OrderItems);
      AddInclude(x => x.DeliveryMethod);

    }
  }
}