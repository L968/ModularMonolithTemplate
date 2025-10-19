namespace ModularMonolithTemplate.Modules.Orders.Application.Features.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<Result<CreateProductResponse>>;
