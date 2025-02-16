﻿using ModularMonolithTemplate.Modules.Products.Domain.Products;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.DeleteProduct;

internal sealed class DeleteProductHandler(
    IProductsDbContext dbContext,
    ILogger<DeleteProductHandler> logger
) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? existingProduct = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (existingProduct is null)
        {
            throw new AppException(ProductErrors.ProductNotFound(request.Id));
        }

        dbContext.Products.Remove(existingProduct);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully deleted Product with Id {Id}", request.Id);
    }
}
