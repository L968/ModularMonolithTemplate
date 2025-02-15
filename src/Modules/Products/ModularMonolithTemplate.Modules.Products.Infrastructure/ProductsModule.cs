using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Domain.Products;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Database;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Products;

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

        services.AddDbContext<ProductsDbContext>(options =>
            options
                .UseMySql(
                    connectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(ProductsDbContext).Assembly.FullName)
                )
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductsDbContext>());

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
