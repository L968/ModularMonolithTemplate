namespace ModularMonolithTemplate.Modules.Orders.Application.Features.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<GetProductByIdResponse>>;
