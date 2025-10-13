using System.Reflection;
using Serilog;
using ModularMonolithTemplate.Api.Extensions;
using ModularMonolithTemplate.Api.Middleware;
using ModularMonolithTemplate.Aspire.ServiceDefaults;
using ModularMonolithTemplate.Common.Application;
using ModularMonolithTemplate.Common.Infrastructure;
using ModularMonolithTemplate.Common.Infrastructure.Extensions;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Orders.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

Assembly[] moduleApplicationAssemblies = [
    ModularMonolithTemplate.Modules.Orders.Application.AssemblyReference.Assembly
];

builder.Services.AddApplication(moduleApplicationAssemblies);

string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow(ServiceNames.Redis);

builder.Services.AddInfrastructure(redisConnectionString);

builder.Configuration.AddModuleConfiguration(["orders"]);

builder.Services.AddOrdersModule(builder.Configuration);

builder.Services.AddHealthChecksConfiguration(builder.Configuration);

builder.Services.AddDocumentation();

builder.Services.AddVersioning();

builder.Host.AddSerilogLogging();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseDocumentation();
}

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

await app.RunAsync();
