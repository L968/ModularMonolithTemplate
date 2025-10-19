using ModularMonolithTemplate.Common.Application;

namespace ModularMonolithTemplate.Modules.Orders.Application.Features.GetProducts;

public sealed record GetProductsQuery(int Page, int PageSize) : IRequest<PaginatedList<GetProductsResponse>>;
