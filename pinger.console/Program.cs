using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pinger.console.Remotes;
using pinger.console.Services;

static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder().Build();
        
        var services = host.Services;
        await services.GetService<PingerService>()?.Run(args)!;
    }

    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                var httpClient = new HttpClient();
                services.AddSingleton(httpClient);
                services.AddTransient<PingerService>();
                services.AddScoped<IActivityRemote, ActivityRemote>();
                services.AddScoped<IHostMonitor, HostMonitor>();
            });
    } 
}