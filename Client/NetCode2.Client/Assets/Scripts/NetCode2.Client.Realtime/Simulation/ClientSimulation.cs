using NetCode2.Client.Realtime.Service;
using NetCode2.Common.Realtime.Data.Commands;

namespace NetCode2.Client.Realtime.Simulation
{
    public class ClientSimulation : IClientSimulation
    {
        private readonly INetworkService networkService;

        public ClientSimulation(INetworkService networkService)
        {
            this.networkService = networkService;
        }

        public void AddCommand(IGameCommand gameCommand)
        {
            networkService.Send(new SimulationCommand(gameCommand));
        }
    }
}