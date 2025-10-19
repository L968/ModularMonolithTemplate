namespace ModularMonolithTemplate.Modules.Orders.Application.Features.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    decimal Price
);
