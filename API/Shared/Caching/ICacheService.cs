using FluentResults;

namespace API.Shared.Caching;

public interface ICacheService
{
    Task<Result<T>> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<Result<T>>> factory,
        TimeSpan? expiration = null,
        bool useSlidingExpiration = false,
        CancellationToken cancellationToken = default);
}