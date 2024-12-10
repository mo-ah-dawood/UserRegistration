using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CvSystem.Core.Repositories;
using CvSystem.Infrastructure.Data;

namespace CvSystem.Infrastructure.Repositories
{
    public class Repository<TEntity>(AppDbContext _context) : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext context = _context;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            return Task.FromResult(entity);
        }

        public IQueryableRepository<TEntity> Query()
        {
            return new QueryableEntities<TEntity>(context.Set<TEntity>());
        }

        public IQueryableRepository<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            return new QueryableEntities<TEntity>(query(context.Set<TEntity>()));
        }



        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public ValueTask<TEntity?> FindAsync(params object?[] keys)
        {
            return context.Set<TEntity>().FindAsync(keys);
        }


    }

    public class QueryableEntities<TEntity>(IQueryable<TEntity> source) : IQueryableRepository<TEntity> where TEntity : class
    {
        public Task<int> CountAsync()
        {
            return source.CountAsync();
        }

        public IQueryableRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            return new QueryableEntities<TEntity>(source.Include(navigationPropertyPath));
        }

        public IQueryableRepository<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            return new QueryableEntities<TEntity>(query(source));
        }

        public async Task<IEnumerable<TEntity>> ToListAsync()
        {
            return await source.ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await source.FirstOrDefaultAsync(predicate);
        }

    }


}