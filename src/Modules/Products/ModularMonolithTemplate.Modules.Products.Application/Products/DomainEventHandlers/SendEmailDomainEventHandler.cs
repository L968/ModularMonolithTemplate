using ModularMonolithTemplate.Common.Application.Messaging;
using ModularMonolithTemplate.Modules.Products.Domain.Products.DomainEvents;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.DomainEventHandlers;

internal class SendEmailDomainEventHandler : DomainEventHandler<ProductCreatedDomainEvent>
{
    public override async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken = default)
    {
        await Task.Delay(2000, cancellationToken);
    }
}
