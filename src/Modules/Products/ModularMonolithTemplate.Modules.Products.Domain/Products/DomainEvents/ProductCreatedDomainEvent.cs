using ModularMonolithTemplate.Common.Domain.DomainEvents;

namespace ModularMonolithTemplate.Modules.Products.Domain.Products.DomainEvents;

public sealed record ProductCreatedDomainEvent(Guid ProductId) : DomainEvent;
