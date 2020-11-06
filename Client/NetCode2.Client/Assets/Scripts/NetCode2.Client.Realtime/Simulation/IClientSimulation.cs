using NetCode2.Common.Realtime.Data.Commands;

namespace NetCode2.Client.Realtime.Simulation
{
    public interface IClientSimulation
    {
        void AddCommand(IGameCommand gameCommand);
    }
}