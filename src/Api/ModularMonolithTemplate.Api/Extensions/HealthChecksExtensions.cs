using ModularMonolithTemplate.Common.Infrastructure;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;

namespace ModularMonolithTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionStringOrThrow(ServiceNames.PostgresDb))
            .AddRedis(configuration.GetConnectionStringOrThrow(ServiceNames.Redis));

        return services;
    }
}
