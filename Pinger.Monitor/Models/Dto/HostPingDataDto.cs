using pinger.console.Models;

namespace Pinger.Monitor.Models.Dto;

public class HostPingDataDto
{
    public List<HostPingResult> PingResults { get; set; }
    public Dictionary<string, PingMetricsModel> HostMetrics { get; set; }
}