using System.Text;
using Newtonsoft.Json;
using pinger.console.Models;

namespace pinger.console.Remotes;

public class ActivityRemote : IActivityRemote
{
    private readonly HttpClient _client;

    private const string Root = "http://localhost:5284/";

    public ActivityRemote(HttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        _client = client;
    }

    public async Task Store(HostPingResult pingResult)
    {
        try
        {
            const string url = $"{Root}api/Activity";
            var content = new StringContent(JsonConvert.SerializeObject(pingResult), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            
            response.EnsureSuccessStatusCode();        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
        
    }
}