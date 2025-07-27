namespace Pinger.Monitor.Models;

public class PingMetricsModel
{
    public double PacketLoss { get; set; }
    public long? MaxPing {get; set; }
    public long? MinPing { get; set; }
    public double? AveragePing { get; set; }
}