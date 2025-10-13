using ModularMonolithTemplate.Common.Application.DomainEvent;
using ModularMonolithTemplate.Modules.Orders.Domain.Products;

namespace ModularMonolithTemplate.Modules.Orders.Application.Products.DomainEventHandlers;

internal sealed class ProductUpdatedDomainEventHandler : DomainEventHandler<ProductUpdatedDomainEvent>
{
    public override Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
