using Aspire.Hosting;

namespace ModularMonolithTemplate.Modules.Products.IntegrationTests;

public sealed class ModularMonolithTemplateApiFixture : IAsyncLifetime
{
    private DistributedApplication? _app;
    private ResourceNotificationService? _resourceNotificationService;
    public HttpClient? HttpClient { get; private set; }

    public async Task InitializeAsync()
    {
        // Arrange
        IDistributedApplicationTestingBuilder appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ModularMonolithTemplate_Aspire_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await appHost.BuildAsync();
        _resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await _app.StartAsync();

        // Create HttpClient and wait for the resource to be running
        HttpClient = _app.CreateHttpClient("modularmonolithtemplate-api");
        await _resourceNotificationService.WaitForResourceAsync("modularmonolithtemplate-api", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
    }

    public async Task DisposeAsync()
    {
        if (_app != null)
        {
            await _app.StopAsync();
            await _app.DisposeAsync();
        }
    }
}
