using MediatR;
using ModularMonolithTemplate.Common.Domain.DomainEvents;

namespace ModularMonolithTemplate.Common.Application;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
