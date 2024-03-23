using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MovieApi.Infrastructure.Common.Services;

namespace Infrastructure.Test.Caching;
public class DistributedCacheService : CacheService<MovieApi.Infrastructure.Caching.DistributedCacheService>
{
    protected override MovieApi.Infrastructure.Caching.DistributedCacheService CreateCacheService() =>
        new(
            new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions())),
            new NewtonSoftService(),
            NullLogger<MovieApi.Infrastructure.Caching.DistributedCacheService>.Instance);
}