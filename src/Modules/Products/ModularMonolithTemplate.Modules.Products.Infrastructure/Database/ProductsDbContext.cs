using Microsoft.EntityFrameworkCore;
using ModularMonolithTemplate.Common.Application.Abstractions;

namespace ModularMonolithTemplate.Modules.Products.Infrastructure.Database;

internal sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options), IUnitOfWork
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
