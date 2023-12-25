using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
            => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<T>()
                    .AsNoTracking() :
                RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                RepositoryContext.Set<T>()
                    .Where(expression);

        public async Task Create(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public IQueryable<T> QueryAll(Expression<Func<T, bool>> predicate = null!) =>
            predicate == null ? RepositoryContext.Set<T>() : RepositoryContext.Set<T>().Where(predicate);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) =>
            await RepositoryContext.Set<T>().CountAsync(predicate);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => 
            await RepositoryContext.Set<T>().AnyAsync(predicate);
        
    }
}
