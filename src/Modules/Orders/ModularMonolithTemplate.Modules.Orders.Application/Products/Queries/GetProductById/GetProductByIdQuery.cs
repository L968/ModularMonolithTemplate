using ModularMonolithTemplate.Common.Domain.Results;

namespace ModularMonolithTemplate.Modules.Orders.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<GetProductByIdResponse>>;
