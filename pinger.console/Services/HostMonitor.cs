using System.Diagnostics;
using System.Net.Sockets;
using Newtonsoft.Json;
using pinger.console.Models;
using pinger.console.Remotes;

namespace pinger.console.Services;

public class HostMonitor : IHostMonitor
{
    private readonly IActivityRemote _activityRemote;
    private const int TimeOut = 3000;

    public HostMonitor(IActivityRemote activityRemote)
    {
        ArgumentNullException.ThrowIfNull(activityRemote, nameof(activityRemote));
        _activityRemote = activityRemote;
    }
    public async Task StartMonitoring(string host, int port, int interval, CancellationToken cancellationToken = default)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await PingHost(host, port);

                var stringValue = JsonConvert.SerializeObject(result);
                Console.WriteLine(stringValue);
                Console.WriteLine("Saving to remote");
                await _activityRemote.Store(result);
                
                if (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(interval, cancellationToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation Cancelled");
        }
    }

    private async Task<HostPingResult> PingHost(string host, int port)
    {
        
        var result = new HostPingResult
        {
            Host = host,
            Port = port,
            Timestamp = DateTime.Now
        };
        
        try
        {
            using var client = new TcpClient();
            var stopwatch = Stopwatch.StartNew();
            var connectTask = client.ConnectAsync(host, port);
                    
            if (await Task.WhenAny(connectTask, Task.Delay(TimeOut)) == connectTask)
            {
                stopwatch.Stop();
                        
                if (client.Connected)
                {
                    result.IsOnline = true;
                    result.PortResponseTime = stopwatch.ElapsedMilliseconds;
                    result.Status = "Open";
                }
                else
                {
                    result.IsOnline = false;
                    result.Status = "Closed";
                }
            }
            else
            {
                result.IsOnline = false;
                result.Status = "Timeout";
                result.ErrorMessage = "Connection timeout";
            }
        }
        catch (Exception ex)
        {
            result.IsOnline = false;
            result.Status = "Error";
            result.ErrorMessage = ex.Message;
        }
        
        return result;
    }
}