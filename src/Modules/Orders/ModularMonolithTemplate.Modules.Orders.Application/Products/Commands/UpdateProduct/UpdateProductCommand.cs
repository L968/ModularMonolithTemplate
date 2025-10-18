namespace ModularMonolithTemplate.Modules.Orders.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price
) : IRequest<Result>;
