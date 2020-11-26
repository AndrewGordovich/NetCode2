using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime
{
    public class Client : IClient
    {
        private readonly ISerializationChannel serializationChannel;

        public ClientId ClientId { get; }

        public Client(ClientId clientId, ISerializationChannel serializationChannel)
        {
            ClientId = clientId;
            this.serializationChannel = serializationChannel;
        }

        public void SendMessage(IPlayerMessage message) => serializationChannel.TryWrite(message);
    }
}