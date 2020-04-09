using System;
using System.Collections.Generic;
using Core.Entities.OrerAggregate;

namespace API.Dto
{
  public class OrderToReturnDto
  {
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public Adress ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
  }
}