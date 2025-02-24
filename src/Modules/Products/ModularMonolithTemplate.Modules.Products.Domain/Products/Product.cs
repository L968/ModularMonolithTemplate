using ModularMonolithTemplate.Common.Domain;
using ModularMonolithTemplate.Common.Domain.DomainEvents;
using ModularMonolithTemplate.Modules.Products.Domain.Products.DomainEvents;

namespace ModularMonolithTemplate.Modules.Products.Domain.Products;

public sealed class Product : EntityHasDomainEvents, IAuditableEntity
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    private Product() { }

    public Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;

        Raise(new ProductCreatedDomainEvent(Id));
    }

    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
