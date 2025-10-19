namespace ModularMonolithTemplate.Modules.Orders.Application.Features.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price
) : IRequest<Result>;
