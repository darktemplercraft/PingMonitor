using Microsoft.Extensions.Caching.Memory;

namespace Pinger.Monitor.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    
    public CacheService(IMemoryCache cache)
    {
        ArgumentNullException.ThrowIfNull(cache, nameof(cache));
        _cache = cache;
    }

    public void Set(string cacheKey, object value)
    {
        _cache.Set(cacheKey, value);
    }

    public T TryGetValue<T>(string cacheKey)
    {
        _cache.TryGetValue(cacheKey, out var cacheObject);
        return (T)cacheObject;
    }

    public void Remove(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }
}