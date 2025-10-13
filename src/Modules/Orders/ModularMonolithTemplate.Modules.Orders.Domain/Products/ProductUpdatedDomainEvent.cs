using ModularMonolithTemplate.Common.Domain.DomainEvents;

namespace ModularMonolithTemplate.Modules.Orders.Domain.Products;

public sealed record ProductUpdatedDomainEvent(Guid ProductId) : DomainEvent;
