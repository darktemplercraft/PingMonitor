namespace pinger.console.Models;

public class HostPingResult
{
    public string Host { get; set; }
    public long Port { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsOnline { get; set; }
    public long? PortResponseTime { get; set; }
    public string Status { get; set; }
    public string ErrorMessage { get; set; }
}