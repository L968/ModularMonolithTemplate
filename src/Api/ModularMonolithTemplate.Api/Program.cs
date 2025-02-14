using System.Reflection;
using ModularMonolithTemplate.Api.Extensions;
using ModularMonolithTemplate.Api.Middleware;
using ModularMonolithTemplate.Aspire.ServiceDefaults;
using ModularMonolithTemplate.Common.Application;
using ModularMonolithTemplate.Common.Presentation.Endpoints;
using ModularMonolithTemplate.Modules.Products.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi();

Assembly[] moduleApplicationAssemblies = [
    ModularMonolithTemplate.Modules.Products.Application.AssemblyReference.Assembly,
];

builder.Services.AddApplication(moduleApplicationAssemblies);

builder.Services.AddProductsModule(builder.Configuration);

builder.Services.AddDocumentation();

builder.Host.AddSerilogLogging();

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseDocumentation();
}

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();
