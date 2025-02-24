using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using Quartz;

namespace ModularMonolithTemplate.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}
