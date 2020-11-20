using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.Runtime
{
    public class Client : IClient
    {
        public ClientId ClientId { get; }

        public Client(ClientId clientId)
        {
            ClientId = clientId;
        }
    }
}