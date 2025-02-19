using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;
using ModularMonolithTemplate.Common.Infrastructure.Interceptors;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Database;

namespace ModularMonolithTemplate.Modules.Products.Infrastructure;

public static class ProductsModule
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionStringOrThrow("modularmonolithtemplate-mysqldb");

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<ProductsDbContext>((serviceProvider, options) =>
            options
                .UseMySql(
                    connectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(ProductsDbContext).Assembly.FullName)
                )
                .AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>())
        );

        services.AddScoped<IProductsDbContext>(sp => sp.GetRequiredService<ProductsDbContext>());
    }
}
