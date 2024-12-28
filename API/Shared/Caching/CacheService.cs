using FluentResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace API.Shared.Caching;

internal sealed class CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    private readonly IMemoryCache _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

    public async Task<Result<T>> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<Result<T>>> factory,
        TimeSpan? expiration = null,
        bool useSlidingExpiration = false,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        var value = await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            if (useSlidingExpiration)
                entry.SetSlidingExpiration(expiration ?? DefaultExpiration);
            else
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

            var result = await factory(cancellationToken);

            // Avoid caching null or default values
            if (!result.IsFailed) return result.Value;
            logger.LogWarning("Factory method returned a default value for key {Key}. Value not cached", key);
            return default;

        });
        return EqualityComparer<T>.Default.Equals(value, default) ? Result.Fail<T>("Value not found") : Result.Ok(value!);
    }
}