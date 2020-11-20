using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Storages;

namespace NetCode2.Server.Realtime.Runtime
{
    public class ClientManager : IClientManager
    {
        private readonly IClientStorage clientStorage;
        private readonly IClientFactory clientFactory;

        public ClientManager(IClientStorage clientStorage, IClientFactory clientFactory)
        {
            this.clientStorage = clientStorage;
            this.clientFactory = clientFactory;
        }

        public void Connect(ClientId clientId)
        {
            var client = clientFactory.Create(clientId);
            clientStorage.Add(clientId, client);
        }

        public void Disconnect(ClientId clientId)
        {
            RemoveClient(clientId);
        }

        private void RemoveClient(ClientId clientId)
        {
            if (clientStorage.Contains(clientId))
            {
                clientStorage.Remove(clientId);
            }
        }

        public void ClientTimeout(ClientId clientId)
        {
            RemoveClient(clientId);
        }

        public bool TryGetClient(ClientId clientId, out IClient client)
        {
            if (clientStorage.Contains(clientId))
            {
                client = clientStorage.Get(clientId);
                return true;
            }

            client = null;
            return false;
        }
    }
}