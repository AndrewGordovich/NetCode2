using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.RoomEngine.Channels.PlayerMessages
{
    public sealed class GameJoinedMessage : IPlayerMessage
    {
        public ClientId ClientId { get; }

        public GameJoinedMessage(ClientId clientId)
        {
            ClientId = clientId;
        }
    }
}