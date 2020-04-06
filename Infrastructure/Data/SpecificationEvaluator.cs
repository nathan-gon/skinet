using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
  {
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
      var query = inputQuery;

      if (spec.Criteria != null)
      {
        query = query.Where(spec.Criteria);// ex)  query.where(x => x.Id==id)

      }
      if (spec.OrderBy != null)
      {
        query = query.OrderBy(spec.OrderBy);// ex)  query.where(x => x.Id==id)

      }
      if (spec.OrderByDescending != null)
      {
        query = query.OrderByDescending(spec.OrderByDescending);// ex)  query.where(x => x.Id==id)

      }
      //paging은 좀 늦게 와야한다 왜냐하면 솔팅을 먼저 해야지 페이징을 하기 때문에 

      if (spec.IsPagingEnabled)
      {
        query = query.Skip(spec.Skip).Take(spec.Take);
      }


      query = spec.Includes
      .Aggregate(query, (current, include) => current.Include(include));


      return query;

    }
  }
}