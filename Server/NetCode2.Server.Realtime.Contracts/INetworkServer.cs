using System.Threading;
using System.Threading.Tasks;

namespace NetCode2.Server.Realtime.Contracts
{
    public interface INetworkServer
    {
        Task Start(CancellationToken cancellationToken);

        Task Stop(CancellationToken cancellationToken);
    }
}