using Scalar.AspNetCore;

namespace ModularMonolithTemplate.Api.Extensions;

internal static class DocumentationExtensions
{
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi();

        return services;
    }

    public static IApplicationBuilder UseDocumentation(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("ModularMonolithTemplate Api")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        return app;
    }
}
