using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
  //All of the classes we use in the GenericRepository are entities 
  //that derive from the BaseEntity . 
  //Since the Product entity derives from the base entity then this matches the
  // constraint here.   We could of course constrain to the class but this is not 
  //really having a constraint at all as then the compiler would accept any class
  // whereas we only want the Entities to be used with the generic repository.
  // 위에 설명과 같이 이렇게 해주면 베이스엔티티를 부모로 갖는 모든엔티티를 반환할수 있게된다
  public interface IGenericRepository<T> where T : BaseEntity
  {
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);
  }
}