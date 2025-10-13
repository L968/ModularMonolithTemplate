using Microsoft.EntityFrameworkCore;
using ModularMonolithTemplate.Common.Application.DomainEvent;
using ModularMonolithTemplate.Common.Domain.DomainEvents;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using ModularMonolithTemplate.Modules.Orders.Infrastructure.Database;

namespace ModularMonolithTemplate.Modules.Orders.Infrastructure.Outbox;

internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated,
    OrdersDbContext dbContext)
    : DomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var outboxMessageConsumer = new OutboxMessageConsumer(domainEvent.Id, decorated.GetType().Name);

        if (await OutboxConsumerExistsAsync(outboxMessageConsumer))
        {
            return;
        }

        await decorated.Handle(domainEvent, cancellationToken);

        dbContext.OutboxMessageConsumers.Add(outboxMessageConsumer);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> OutboxConsumerExistsAsync(OutboxMessageConsumer outboxMessageConsumer)
    {
        return await dbContext.OutboxMessageConsumers
            .AsNoTracking()
            .AnyAsync(omc => omc.OutboxMessageId == outboxMessageConsumer.OutboxMessageId && omc.Name == outboxMessageConsumer.Name);
    }
}
