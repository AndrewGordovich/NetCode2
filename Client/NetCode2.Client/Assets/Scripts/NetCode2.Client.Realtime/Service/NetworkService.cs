using NetCode2.Client.Realtime.Connection;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Serialization;

namespace NetCode2.Client.Realtime.Service
{
    public class NetworkService : INetworkService
    {
        private readonly ICommunication communication;

        private SimulationCommandsSerializer commandsSerializer = new SimulationCommandsSerializer();

        public NetworkService(ICommunication communication)
        {
            this.communication = communication;
        }

        public void Send()
        {
            communication.ServiceOnce();
        }

        public void Send(SimulationCommand command)
        {
            var data = commandsSerializer.Serialize(command);
            communication.SendReliable(data, data.Length);

            communication.ServiceOnce();
        }
    }
}