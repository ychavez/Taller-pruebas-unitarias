using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<Order> Orders { get; set; }

        private Guid TenantId;

        private static LambdaExpression MultiTenantExpression<T>(OrderContext context)
            where T : EntityBase, IMultiTenant
        { 
            Expression<Func<T,bool>> tenantFilter = x => x.TenantId == context.TenantId;
            return tenantFilter;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entity.ClrType;

                if (!typeof(IMultiTenant).IsAssignableFrom(entityType)) continue;

                var method = typeof(OrderContext).GetMethod(nameof(MultiTenantExpression),
                    BindingFlags.NonPublic|
                    BindingFlags.Static)?.MakeGenericMethod(entityType);

                var filter = method?.Invoke(null, [this]);

                entity.SetQueryFilter((LambdaExpression)filter!);

                entity.AddIndex(entity.FindProperty(nameof(IMultiTenant.TenantId))!);
            }

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "User";
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "User";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break; 
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
