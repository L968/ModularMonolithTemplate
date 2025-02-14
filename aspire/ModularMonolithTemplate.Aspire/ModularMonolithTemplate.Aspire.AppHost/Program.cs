IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> mysqlPassword = builder.AddParameter("mysqlPassword", "root", secret: true);

IResourceBuilder<MySqlServerResource> mysql = builder.AddMySql("modularmonolithtemplate-mysql", password: mysqlPassword)
    .WithImageTag("9.2.0")
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<MySqlDatabaseResource> mysqldb = mysql.AddDatabase("modularmonolithtemplate-mysqldb", "modularmonolithtemplate");

builder.AddProject<Projects.ModularMonolithTemplate_Api>("modularmonolithtemplate-api")
    .WithReference(mysqldb)
    .WaitFor(mysqldb);

//builder.AddProject<Projects.ModularMonolithTemplate_MigrationService>("modularmonolithtemplate-migrationservice")
//    .WithReference(mysqldb)
//    .WaitFor(mysqldb)

await builder.Build().RunAsync();
