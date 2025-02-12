namespace ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResponse>;
