namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    decimal Price
);
