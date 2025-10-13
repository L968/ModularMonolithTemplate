using Microsoft.Extensions.Caching.Distributed;

namespace ModularMonolithTemplate.Common.Infrastructure.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
        expiration is null
            ? DefaultExpiration
            : new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
}
