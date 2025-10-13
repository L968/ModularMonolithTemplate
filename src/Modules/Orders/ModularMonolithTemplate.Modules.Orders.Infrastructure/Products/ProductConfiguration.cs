using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularMonolithTemplate.Modules.Orders.Domain.Products;

namespace ModularMonolithTemplate.Modules.Orders.Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
