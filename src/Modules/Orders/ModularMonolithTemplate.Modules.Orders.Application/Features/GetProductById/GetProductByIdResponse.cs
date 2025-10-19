namespace ModularMonolithTemplate.Modules.Orders.Application.Features.GetProductById;

public sealed record GetProductByIdResponse(
    Guid Id,
    string Name,
    decimal Price
);
