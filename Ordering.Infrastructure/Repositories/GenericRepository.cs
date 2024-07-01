using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepository<T>(OrderContext orderContext) : 
        IGenericRepository<T> where T : EntityBase
    {
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
            => await orderContext
                                .Set<T>()
                                .Where(predicate)
                                .ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(int offset, int limit,
            Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            params string[] includeString)
        {
            IQueryable<T> query = orderContext.Set<T>();

            query.Skip(offset).Take(limit);

            query = includeString.Aggregate(query, (current, itemInclude) => current.Include(itemInclude));

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();

        }

        public async Task<T> AddAsync(T item)
        {
            await orderContext.AddAsync(item);
            await orderContext.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(T item)
        {
            orderContext.Update(item);
            await orderContext.SaveChangesAsync();
        }
    }
}
