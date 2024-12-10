using System.Linq.Expressions;

namespace CvSystem.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> FindAsync(params object?[] keys);
        IQueryableRepository<TEntity> Query();
        IQueryableRepository<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }

    public interface IQueryableRepository<TEntity> where TEntity : class
    {
        public IQueryableRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);
        public Task<int> CountAsync();
        public Task<IEnumerable<TEntity>> ToListAsync();
        IQueryableRepository<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    }

}