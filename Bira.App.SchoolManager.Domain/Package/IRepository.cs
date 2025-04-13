using System.Linq.Expressions;

namespace Bira.App.SchoolManager.Domain.Package
{
    public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(int code);
        Task<TEntity> GetIncludeById(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TEntity>> GetIncludeAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes);
        Task Update(TEntity entity);
        Task Delete(int code);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}