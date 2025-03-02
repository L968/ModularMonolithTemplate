﻿using ModularMonolithTemplate.Modules.Products.Application.Products.Commands.UpdateProduct;

namespace ModularMonolithTemplate.Modules.Products.Presentation.Products.v1;

internal sealed class UpdateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("product/{id:Guid}", async (Guid id, UpdateProductCommand command, ISender sender) =>
        {
            command.Id = id;
            await sender.Send(command);

            return Results.NoContent();
        })
        .WithTags(Tags.Products)
        .MapToApiVersion(1);
    }
}
