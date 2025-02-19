using MediatR;

namespace ModularMonolithTemplate.Common.Domain.DomainEvents;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
