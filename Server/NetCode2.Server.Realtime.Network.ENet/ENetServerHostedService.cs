using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetCode2.Server.Realtime.Contracts;
using ENetLib = ENet.Library;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public class ENetServerHostedService : IHostedService, IDisposable
    {
        private readonly INetworkServer networkServer;

        public ENetServerHostedService(INetworkServer networkServer)
        {
            this.networkServer = networkServer;
            ENetLib.Initialize();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await networkServer.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await networkServer.Stop(cancellationToken);
        }

        public void Dispose()
        {
            ENetLib.Deinitialize();
        }
    }
}