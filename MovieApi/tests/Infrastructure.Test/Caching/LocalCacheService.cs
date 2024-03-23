using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;
public class LocalCacheService : CacheService<MovieApi.Infrastructure.Caching.LocalCacheService>
{
    protected override MovieApi.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<MovieApi.Infrastructure.Caching.LocalCacheService>.Instance);
}