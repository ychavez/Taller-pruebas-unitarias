using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T item);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(int offset, int limit, Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, params string[] includeString);
        Task UpdateAsync(T item);
    }
}