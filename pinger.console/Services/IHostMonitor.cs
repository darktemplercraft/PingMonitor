namespace pinger.console.Services;

public interface IHostMonitor
{
    Task StartMonitoring(string host, int port, int interval, CancellationToken cancellationToken = default);
}