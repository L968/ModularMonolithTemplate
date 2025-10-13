using ModularMonolithTemplate.Modules.Orders.Domain.Products;

namespace ModularMonolithTemplate.Modules.Orders.Application.Abstractions;

public interface IOrdersDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
