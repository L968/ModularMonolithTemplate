namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<CreateProductResponse>;
