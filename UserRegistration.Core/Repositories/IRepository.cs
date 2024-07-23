using System.Linq.Expressions;

namespace UserRegistration.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> Find(params object?[] keys);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>>  QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public void DeleteMany(params TEntity[] entities);
    }
}