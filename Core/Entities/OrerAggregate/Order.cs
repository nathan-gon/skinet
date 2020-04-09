using System;
using System.Collections.Generic;

namespace Core.Entities.OrerAggregate
{
  public class Order : BaseEntity
  {
    public Order()
    {
    }

    public Order(string buyerEmail, Adress shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal)
    {
      //나머지는 초기값을 줬기 때문에 안넣어줘도 된다
      BuyerEmail = buyerEmail;
      ShipToAddress = shipToAddress;
      DeliveryMethod = deliveryMethod;
      OrderItems = orderItems;
      Subtotal = subtotal;
    }

    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public Adress ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; }
    public decimal GetTotal()
    {
      return Subtotal + DeliveryMethod.Price;
    }


  }
}