using System.Reflection;
using ModularMonolithTemplate.Api.Extensions;
using ModularMonolithTemplate.Api.Middleware;
using ModularMonolithTemplate.Aspire.ServiceDefaults;
using ModularMonolithTemplate.Common.Application;
using ModularMonolithTemplate.Common.Infrastructure;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Products.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

Assembly[] moduleApplicationAssemblies = [
    ModularMonolithTemplate.Modules.Products.Application.AssemblyReference.Assembly,
];

builder.Services.AddApplication(moduleApplicationAssemblies);

builder.Services.AddInfrastructure();

builder.Configuration.AddModuleConfiguration(["products"]);

builder.Services.AddProductsModule(builder.Configuration);

builder.Services.AddHealthChecksConfiguration(builder.Configuration);

builder.Services.AddOpenApi();

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
