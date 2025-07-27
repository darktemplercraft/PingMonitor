using pinger.console.Models;

namespace pinger.console.Remotes;

public interface IActivityRemote
{
    Task Store(HostPingResult pingResult);
}