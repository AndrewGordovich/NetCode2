using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;

namespace NetCode2.Server.Realtime.Runtime
{
    public class ClientFactory : IClientFactory
    {
        private readonly ISerializationChannel serializationChannel;

        public ClientFactory(ISerializationChannel serializationChannel)
        {
            this.serializationChannel = serializationChannel;
        }

        public IClient Create(ClientId clientId) => new Client(clientId, serializationChannel);
    }
}