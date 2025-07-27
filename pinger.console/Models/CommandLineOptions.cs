namespace pinger.console.Models;

public class CommandLineOptions
{
    public List<string> Hosts { get; set; } = new List<string>();
    public int Port { get; set; }
    public int Interval { get; set; }
}