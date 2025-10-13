using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using ModularMonolithTemplate.Modules.Orders.Application.Abstractions;
using ModularMonolithTemplate.Modules.Orders.Domain.Products;
using ModularMonolithTemplate.Modules.Orders.Infrastructure.Products;

namespace ModularMonolithTemplate.Modules.Orders.Infrastructure.Database;

public sealed class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options), IOrdersDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Orders);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }
}
