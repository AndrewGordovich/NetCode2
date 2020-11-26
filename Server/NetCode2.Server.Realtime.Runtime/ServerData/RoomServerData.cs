using NetCode2.Server.Common.Meta.Communication;

namespace NetCode2.Server.Realtime.Runtime.ServerData
{
    public class RoomServerData
    {
        public RoomEngine.Gameplay.RoomEngine RoomEngine { get; set; }

        public RoomMetaData RoomMetaData { get; set; }
    }
}