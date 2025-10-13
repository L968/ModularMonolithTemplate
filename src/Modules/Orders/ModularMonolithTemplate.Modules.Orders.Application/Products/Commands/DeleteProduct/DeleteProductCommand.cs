using ModularMonolithTemplate.Common.Domain.Results;

namespace ModularMonolithTemplate.Modules.Orders.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
