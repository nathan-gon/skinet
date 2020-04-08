using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class StoreContext : DbContext
  {
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



      //decimal타입을 시퀄라이트가 지원을 안해서 에러 제거 해줬다 
      //다른서버를 쓴다면 생기지 않을것이다 그러나 이것도 괜찮다 
      if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
      {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
          var properties = entityType.ClrType.GetProperties()
                .Where(x => x.PropertyType == typeof(decimal));

          foreach (var property in properties)
          {
            modelBuilder.Entity(entityType.Name).Property(property.Name)
            .HasConversion<double>();
          }
        }
      }

    }

  }
}