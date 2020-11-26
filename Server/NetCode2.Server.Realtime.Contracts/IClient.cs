using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Contracts
{
    public interface IClient
    {
        ClientId ClientId { get; }

        void SendMessage(IPlayerMessage message);
    }
}