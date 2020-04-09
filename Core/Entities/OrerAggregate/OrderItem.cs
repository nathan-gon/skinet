namespace Core.Entities.OrerAggregate
{
  public class OrderItem : BaseEntity
  {
    //엔티티프레임워크는 엔티티에 빈 생성자가 필요하다 그래서 만듬
    public OrderItem()
    {
    }

    public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
    {
      ItemOrdered = itemOrdered;
      Price = price;
      Quantity = quantity;
    }

    public ProductItemOrdered ItemOrdered { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
  }
}