using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.RoomEngine.Channels.RoomMessages
{
    public class JoinRoomMessage : IRoomServiceMessage
    {
        public IClient Client { get; }

        public JoinRoomMessage(IClient client)
        {
            Client = client;
        }
    }
}