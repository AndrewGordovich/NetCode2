using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public class RoomPlayer
    {
        private IClient client;

        public ClientId ClientId => client.ClientId;

        public RoomPlayer(IClient client)
        {
            this.client = client;
        }

        public void SendMessage(IPlayerMessage message) => client.SendMessage(message);
    }
}