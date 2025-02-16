using ModularMonolithTemplate.Modules.Products.Domain.Products;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(
    IProductsDbContext dbContext,
    ILogger<UpdateProductHandler> logger
) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (product is null)
        {
            throw new AppException(ProductErrors.ProductNotFound(request.Id));
        }

        product.Update(
            request.Name,
            request.Price
        );

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully updated {@Product}", product);
    }
}
