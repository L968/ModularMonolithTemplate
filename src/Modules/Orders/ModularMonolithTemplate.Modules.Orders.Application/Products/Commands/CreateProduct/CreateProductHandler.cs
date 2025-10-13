using ModularMonolithTemplate.Common.Domain.Results;
using ModularMonolithTemplate.Modules.Orders.Application.Abstractions;
using ModularMonolithTemplate.Modules.Orders.Domain.Products;

namespace ModularMonolithTemplate.Modules.Orders.Application.Products.Commands.CreateProduct;

internal sealed class CreateProductHandler(
    IOrdersDbContext dbContext,
    ILogger<CreateProductHandler> logger
) : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool existingProduct = await dbContext.Products.AnyAsync(p => p.Name == request.Name, cancellationToken);
        if (existingProduct)
        {
            return Result.Failure(ProductErrors.ProductAlreadyExists(request.Name));
        }

        var product = new Product(
            request.Name,
            request.Price
        );

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Successfully created {@Product}", product);

        return Result.Success(
            new CreateProductResponse(
                product.Id,
                product.Name,
                product.Price
            )
        );
    }
}
