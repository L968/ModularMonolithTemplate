﻿using ModularMonolithTemplate.Modules.Orders.Application.Abstractions;
using ModularMonolithTemplate.Modules.Orders.Domain.Products;

namespace ModularMonolithTemplate.Modules.Orders.Application.Products.Queries.GetProductById;

internal sealed class GetProductByIdHandler(
    IOrdersDbContext dbContext,
    ILogger<GetProductByIdHandler> logger
) : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
{
    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.Id));
        }

        logger.LogDebug("Successfully retrieved Product with Id {Id}", request.Id);

        var response = new GetProductByIdResponse(
            product.Id,
            product.Name,
            product.Price
        );

        return Result.Success(response);
    }
}
