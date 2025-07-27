using pinger.console.Models;
using Pinger.Monitor.Models;
using Pinger.Monitor.Models.Dto;

namespace Pinger.Monitor.Services;

public class ActivityService :IActivityService
{
    private readonly ICacheService _cacheService;
    private const string CacheKey = "PingActivity";

    public ActivityService(ICacheService cacheService)
    {
        ArgumentNullException.ThrowIfNull(cacheService, nameof(cacheService));
        _cacheService = cacheService;
    }

    public void Store(HostPingResult pingResult)
    {
        var results = GetPingResults();
        results.Add(pingResult);
        
        _cacheService.Set(CacheKey, results);   
    }

    public HostPingDataDto GetData()
    {
        var pingData =  new HostPingDataDto();
        var pingResults = GetPingResults();
        pingData.PingResults = pingResults;
        pingData.HostMetrics = GenerateStatistics(pingResults);
        return pingData;
    }

    private Dictionary<string, PingMetricsModel> GenerateStatistics(List<HostPingResult> pingResults)
    {
        var hostPingMetrics = new Dictionary<string, PingMetricsModel>();
        if (pingResults == null)
        {
            return hostPingMetrics;
        }
        
        var groupedByHost = pingResults.GroupBy(x => x.Host);
        foreach (var group in groupedByHost)
        {
            var totalAttempts = group.Count();
            var totalSuccess = group.Count(_ => _.IsOnline);
            var packetLoss = totalAttempts > 0 ? (double)(totalAttempts - totalSuccess) / totalAttempts * 100 : 0;
            var maxPing = group.Max(_ => _.PortResponseTime);
            var minPing = group.Min(_ => _.PortResponseTime);
            var averagePing = group.Average(_ => _.PortResponseTime);
            var pingMetrics = new PingMetricsModel
            {
                PacketLoss = packetLoss,
                MaxPing = maxPing,
                MinPing = minPing,
                AveragePing = averagePing
            };
            hostPingMetrics.Add(group.Key, pingMetrics);
        }
        
        return hostPingMetrics;
    }

    private List<HostPingResult> GetPingResults()
    {
        var results = _cacheService.TryGetValue<List<HostPingResult>>(CacheKey);
        if (results == null)
        {
            return new List<HostPingResult>();
        }

        return results.OrderByDescending(_ => _.Timestamp).ToList();
    }
}