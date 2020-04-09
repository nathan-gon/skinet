using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly StoreContext _context;
    private Hashtable _repositories;
    public UnitOfWork(StoreContext context)
    {
      _context = context;
    }

    public async Task<int> Complete()
    {
      return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
      if (_repositories == null)
      {
        _repositories = new Hashtable();
      }
      var type = typeof(TEntity).Name;

      if (!_repositories.ContainsKey(type))
      {
        var repositoryType = typeof(GenericRepository<>);
        //여러개의 콘텍스트를 복사하는것이아니라 각각의 레포지에 콘텍스트를 인자로 전달함
        var repositoryInstance = Activator
        .CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        //hashTable에 키와 벨류를 저장함 키는 이름 벨류는 레포인스턴스
        _repositories.Add(type, repositoryInstance);
      }
      return (IGenericRepository<TEntity>)_repositories[type];






    }
  }
}