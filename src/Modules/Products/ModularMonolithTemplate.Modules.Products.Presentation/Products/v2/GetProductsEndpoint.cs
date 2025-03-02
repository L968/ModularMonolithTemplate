using Microsoft.AspNetCore.Mvc;
using ModularMonolithTemplate.Common.Application;
using ModularMonolithTemplate.Modules.Products.Application.Products.Queries.GetProducts;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products.v2;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
                ISender sender,
                CancellationToken cancellationToken,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10) =>
            {
                var query = new GetProductsQuery(page, pageSize);
                PaginatedList<GetProductsResponse> response = await sender.Send(query, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Products)
            .MapToApiVersion(2);
    }
}
