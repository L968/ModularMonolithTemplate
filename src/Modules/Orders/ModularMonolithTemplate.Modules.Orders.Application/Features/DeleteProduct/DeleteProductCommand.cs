namespace ModularMonolithTemplate.Modules.Orders.Application.Features.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
