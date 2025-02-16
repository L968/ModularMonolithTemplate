﻿using ModularMonolithTemplate.Modules.Products.Domain.Products;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProductById;

internal sealed class GetProductByIdHandler(
    IProductsDbContext dbContext,
    ILogger<GetProductByIdHandler> logger
) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new AppException(ProductErrors.ProductNotFound(request.Id));
        }

        logger.LogInformation("Successfully retrieved  Product with Id {Id}", request.Id);

        return new GetProductByIdResponse(
            product.Id,
            product.Name,
            product.Price
        );
    }
}
