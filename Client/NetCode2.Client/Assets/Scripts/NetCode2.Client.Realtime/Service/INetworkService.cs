using NetCode2.Common.Realtime.Data.Commands;

namespace NetCode2.Client.Realtime.Service
{
    public interface INetworkService
    {
        void Send();
        void Send(SimulationCommand simulationCommand);
    }
}