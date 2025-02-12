using ModularMonolithTemplate.Modules.Products.Application.Products.Commands.CreateProduct;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products;

internal sealed class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("product", async (CreateProductCommand command, ISender sender) =>
        {
            CreateProductResponse response = await sender.Send(command);

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithTags(Tags.Products);
    }
}
