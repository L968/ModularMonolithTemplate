IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> postgresPassword = builder.AddParameter("postgresPassword", "root", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("modularmonolithtemplate-postgres", password: postgresPassword)
    .WithImageTag("17.4")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<PostgresDatabaseResource> productsDb = postgres.AddDatabase("modularmonolithtemplate-postgresdb", "modularmonolithtemplate");

builder.AddProject<Projects.ModularMonolithTemplate_Api>("modularmonolithtemplate-api")
    .WithReference(productsDb)
    .WaitFor(productsDb);

builder.AddProject<Projects.ModularMonolithTemplate_Modules_Products_MigrationService>("modularmonolithtemplate-migrationservice")
    .WithReference(productsDb)
    .WaitFor(productsDb);

await builder.Build().RunAsync();
