using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModularMonolithTemplate.Common.Domain.DomainEvents;
using ModularMonolithTemplate.Common.Infrastructure.Outbox;
using ModularMonolithTemplate.Common.Infrastructure.Serialization;
using ModularMonolithTemplate.Modules.Products.Infrastructure.Database;
using Newtonsoft.Json;
using Quartz;

namespace ModularMonolithTemplate.Modules.Products.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxJob(
    ProductsDbContext dbContext,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = "Products";

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        List<OutboxMessage> outboxMessages = await GetUnprocessedOutboxMessagesAsync();

        logger.LogInformation("{Module} - Found {Count} unprocessed messages", ModuleName, outboxMessages.Count);

        foreach (OutboxMessage outboxMessage in outboxMessages)
        {
            logger.LogInformation("{Module} - Processing outbox message {MessageId}", ModuleName, outboxMessage.Id);

            Exception? exception = null;

            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Payload,
                    SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(domainEvent);
                logger.LogInformation("{Module} - Successfully processed message {MessageId}", ModuleName, outboxMessage.Id);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "{Module} - Exception while processing outbox message {MessageId}",
                    ModuleName,
                    outboxMessage.Id);

                exception = caughtException;
            }

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            outboxMessage.Error = exception?.ToString();
        }

        await dbContext.SaveChangesAsync();

        logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }

    public async Task<List<OutboxMessage>> GetUnprocessedOutboxMessagesAsync()
    {
        return await dbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(outboxOptions.Value.BatchSize)
            .ToListAsync();
    }
}
