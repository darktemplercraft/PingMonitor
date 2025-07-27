namespace Pinger.Monitor.Services;

public interface ICacheService
{
    void Set(string cacheKey, object value);
    T TryGetValue<T>(string cacheKey);
    void Remove(string cacheKey);
}