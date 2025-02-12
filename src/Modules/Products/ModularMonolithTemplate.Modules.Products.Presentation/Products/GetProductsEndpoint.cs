using ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProducts;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender) =>
        {
            var query = new GetProductsQuery();
            IEnumerable<GetProductsResponse> response = await sender.Send(query);

            return Results.Ok(response);
        })
        .WithTags(Tags.Products);
    }
}
