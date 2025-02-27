using ModularMonolithTemplate.Common.Application.Messaging;
using ModularMonolithTemplate.Modules.Products.Domain.Products.DomainEvents;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.DomainEventHandlers;

internal sealed class ProductCreatedDomainEventHandler : DomainEventHandler<ProductCreatedDomainEvent>
{
    public override Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
