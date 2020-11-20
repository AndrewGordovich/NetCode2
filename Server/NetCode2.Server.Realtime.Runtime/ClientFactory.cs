using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.Runtime
{
    public class ClientFactory : IClientFactory
    {
        public IClient Create(ClientId clientId) => new Client(clientId);
    }
}