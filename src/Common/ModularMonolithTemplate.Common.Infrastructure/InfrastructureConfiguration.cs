using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularMonolithTemplate.Common.Infrastructure.Interceptors;

namespace ModularMonolithTemplate.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<PublishDomainEventsInterceptor>();
    }
}
