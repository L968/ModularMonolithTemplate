using Microsoft.EntityFrameworkCore;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Domain.Products;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Products;

namespace ModularMonolithTemplate.Modules.Products.Infrastructure.Database;

public sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options), IProductsDbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Product);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
