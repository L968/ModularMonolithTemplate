using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModularMonolithTemplate.Common.Domain;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Domain.Products;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Products;

namespace ModularMonolithTemplate.Modules.Products.Infrastructure.Database;

public sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options), IProductsDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Products);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries = ChangeTracker.Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
            DateTime utcNow = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Property(e => e.CreatedAtUtc).CurrentValue = utcNow;
                entry.Property(e => e.UpdatedAtUtc).CurrentValue = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.UpdatedAtUtc).CurrentValue = utcNow;
            }
        }
    }
}
