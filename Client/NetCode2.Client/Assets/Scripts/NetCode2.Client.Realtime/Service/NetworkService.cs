using System;
using System.Collections.Generic;
using NetCode2.Client.Realtime.Connection;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Serialization;
using NetCode2.Common.Realtime.Service;

namespace NetCode2.Client.Realtime.Service
{
    public class NetworkService : INetworkService
    {
        private readonly ICommunication communication;
        private readonly Dictionary<NetworkDataCode, IDataHandler> dataHandlers;

        private SimulationCommandsSerializer commandsSerializer = new SimulationCommandsSerializer();

        private readonly IDictionary<Type, NetworkDataCode> commandDataToNetworkDataCode =
            new Dictionary<Type, NetworkDataCode>
            {
                {typeof(JoinGameCommandData), NetworkDataCode.JoinGameCommand}
            };

        private readonly IDictionary<NetworkDataCode, IDataSerializer> commandSerializers =
            new Dictionary<NetworkDataCode, IDataSerializer>
            {
                {NetworkDataCode.JoinGameCommand, new JoinGameCommandDataSerializer()}
            };

        public NetworkService(ICommunication communication, Dictionary<NetworkDataCode, IDataHandler> dataHandlers)
        {
            this.communication = communication;
            this.dataHandlers = dataHandlers;
        }

        public void QueueAndSendCommand(IServiceCommandData commandData)
        {
            var dataCode = commandDataToNetworkDataCode[commandData.GetType()];
            QueueAndSendCommandData(dataCode, commandData);
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

        public void Receive()
        {
            communication.ServiceAll();

            while (communication.HasData())
            {
                var data = communication.GetData();
                if (dataHandlers.ContainsKey(data.Code))
                {
                    var handler = dataHandlers[data.Code];
                    handler.Handle(data);
                    data.Dispose();
                }
            }
        }

        private void QueueAndSendCommandData(NetworkDataCode dataCode, object commandData)
        {
            var serializer = commandSerializers[dataCode];
            var data = serializer.Serialize(commandData);
            communication.SendReliable(data, data.Length);
        }
    }
}