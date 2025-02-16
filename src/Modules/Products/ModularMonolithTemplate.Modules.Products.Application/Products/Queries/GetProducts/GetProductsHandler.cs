using ModularMonolithTemplate.Modules.Products.Domain.Products;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(
    IProductsDbContext dbContext,
    ILogger<GetProductsHandler> logger
) : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await dbContext.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = products
            .Select(p => new GetProductsResponse(
                p.Id,
                p.Name,
                p.Price
            ))
            .ToList();

        logger.LogInformation("Successfully retrieved {Count} products", response.Count);

        return response;
    }
}
