using Microsoft.AspNetCore.Routing;

namespace ModularMonolithTemplate.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
