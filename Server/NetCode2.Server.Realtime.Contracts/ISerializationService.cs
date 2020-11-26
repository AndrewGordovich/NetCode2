using System.Threading;
using System.Threading.Tasks;

namespace NetCode2.Server.Realtime.Contracts
{
    public interface ISerializationService
    {
        Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);
    }
}