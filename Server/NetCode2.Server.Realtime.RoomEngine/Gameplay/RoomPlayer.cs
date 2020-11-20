using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public class RoomPlayer
    {
        private IClient client;

        public RoomPlayer(IClient client)
        {
            this.client = client;
        }
    }
}