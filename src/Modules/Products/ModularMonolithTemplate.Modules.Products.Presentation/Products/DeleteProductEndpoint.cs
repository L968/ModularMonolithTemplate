using ModularMonolithTemplate.Modules.Products.Application.Products.Commands.DeleteProduct;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products;

internal sealed class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("product/{id:Guid}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new DeleteProductCommand(id));
            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}
