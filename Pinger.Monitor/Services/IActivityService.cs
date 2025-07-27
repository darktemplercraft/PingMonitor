using pinger.console.Models;
using Pinger.Monitor.Models.Dto;

namespace Pinger.Monitor.Services;

public interface IActivityService
{
    void Store(HostPingResult pingResult);
    HostPingDataDto GetData();
}