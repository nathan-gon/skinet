using System;
using Core.Entities.OrerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.OwnsOne(x => x.ShipToAddress, a =>
      {
        a.WithOwner();

      });
      builder.Property(x => x.Status)
            .HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
            );
      builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

    }
  }
}