using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Database;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Outbox;

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
        string dbConnectionString = configuration.GetConnectionStringOrThrow("modularmonolithtemplate-postgresdb");

        services.AddDbContext<ProductsDbContext>((serviceProvider, options) =>
            options
                .UseNpgsql(
                    connectionString: dbConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Products)
                )
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>())
        );

        services.AddScoped<IProductsDbContext>(sp => sp.GetRequiredService<ProductsDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("Products:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
