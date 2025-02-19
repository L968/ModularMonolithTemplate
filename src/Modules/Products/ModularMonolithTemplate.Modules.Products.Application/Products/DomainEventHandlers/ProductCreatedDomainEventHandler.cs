using ModularMonolithTemplate.Common.Application;
using ModularMonolithTemplate.Modules.Products.Domain.Products.DomainEvents;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.DomainEventHandlers;

internal class ProductCreatedDomainEventHandler : IDomainEventHandler<ProductCreatedDomainEvent>
{
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
