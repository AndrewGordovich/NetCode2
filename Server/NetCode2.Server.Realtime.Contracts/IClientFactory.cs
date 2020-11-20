namespace NetCode2.Server.Realtime.Contracts
{
    public interface IClientFactory
    {
        IClient Create(ClientId clientId);
    }
}