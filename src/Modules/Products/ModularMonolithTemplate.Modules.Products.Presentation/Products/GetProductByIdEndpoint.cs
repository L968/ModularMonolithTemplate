using ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProductById;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products;

internal sealed class GetProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("product/{id:Guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            GetProductByIdResponse? response = await sender.Send(query);

            return response is not null
                ? Results.Ok(response)
                : Results.NotFound();
        })
        .WithTags(Tags.Products);
    }
}
