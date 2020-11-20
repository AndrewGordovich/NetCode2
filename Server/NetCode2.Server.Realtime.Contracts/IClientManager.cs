namespace NetCode2.Server.Realtime.Contracts
{
    public interface IClientManager
    {
        void Connect(ClientId clientId);

        void Disconnect(ClientId clientId);

        void ClientTimeout(ClientId clientId);

        bool TryGetClient(ClientId clientId, out IClient client);
    }
}