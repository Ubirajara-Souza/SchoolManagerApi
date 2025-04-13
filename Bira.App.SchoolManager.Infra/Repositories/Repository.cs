using Bira.App.SchoolManager.Domain.Package;
using Bira.App.SchoolManager.Infra.Repositories.BaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Bira.App.SchoolManager.Infra.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly ApiDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApiDbContext _context)
        {
            Context = _context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.AsNoTracking().Where(x => x.DateDeactivation == null).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetIncludeAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.AsNoTracking().Where(x => x.DateDeactivation == null).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int code)
        {
            return await DbSet.FindAsync(code);
        }

        public virtual async Task<TEntity> GetIncludeById(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().Where(x => x.DateDeactivation == null).SingleOrDefaultAsync(predicate);
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(int code)
        {
            DbSet.Remove(new TEntity { Code = code });
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}