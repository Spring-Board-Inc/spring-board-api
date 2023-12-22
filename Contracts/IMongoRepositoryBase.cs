namespace Contracts
{
    public interface IMongoRepositoryBase<TCollection>
    {
        Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression);
        Task CreateAsync(TCollection newDocument);
        Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression);
        IQueryable<TCollection> GetAsQueryable(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression);
        Task<List<TCollection>> GetAsync();
        Task<TCollection?> GetAsync(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression);
        Task RemoveAsync(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression);
        Task UpdateAsync(System.Linq.Expressions.Expression<Func<TCollection, bool>> expression, TCollection updateDocument);
    }
}
