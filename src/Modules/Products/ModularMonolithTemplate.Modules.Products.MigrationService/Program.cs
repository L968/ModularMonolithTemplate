using ModularMonolithTemplate.Aspire.ServiceDefaults;
using ModularMonolithTemplate.Common.Infrastructure;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Database;
using ModularMonolithTemplate.Modules.Products.MigrationService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<ProductsDbContext>(ServiceNames.PostgresDb);

IHost host = builder.Build();
await host.RunAsync();
