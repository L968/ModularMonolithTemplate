namespace ModularMonolithTemplate.Common.Domain.DomainEvents;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
