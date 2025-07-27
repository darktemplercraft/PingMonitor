using pinger.console.Helpers;

namespace pinger.console.Services;

public class PingerService : IPingerService
{
    private readonly IHostMonitor _hostMonitor;

    public PingerService(IHostMonitor hostMonitor)
    {
        ArgumentNullException.ThrowIfNull(hostMonitor, nameof(hostMonitor));
        _hostMonitor = hostMonitor;
    }

    public async Task Run(string[] args)
    {
        try
        {
            var options = DataHelper.ParseCommandLineArgs(args);
            var cts = new CancellationTokenSource();

            // Handle Ctrl+C gracefully
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            foreach (var host in options.Hosts)
            {
                Task.Run(async () => { await StartMonitoring(host, options.Port, options.Interval, cts.Token); },
                    cts.Token);
            }

            while (!cts.Token.IsCancellationRequested)
            {
                await Task.Delay(1000, cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation Cancelled");
        }
    }
    
    private async Task StartMonitoring(string host, int port, int interval, CancellationToken ct)
    {
        Console.WriteLine($"Starting monitoring at {host}:{port}");
        await _hostMonitor.StartMonitoring(host, port, interval, ct);
    }
}