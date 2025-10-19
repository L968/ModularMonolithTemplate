namespace ModularMonolithTemplate.Modules.Orders.Application.Features.GetProducts;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    decimal Price
);
