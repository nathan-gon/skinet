using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IProductRepository
  {
    Task<Product> GetProductByIdAsync(int id);

    // 이거는 그냥 리턴만 하는거고 로직이 필요하지 않기 때문에 그냥 IREadonly로 리턴
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
  }
}