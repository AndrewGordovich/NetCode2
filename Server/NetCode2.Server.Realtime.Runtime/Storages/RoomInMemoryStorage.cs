using NetCode2.Common;
using NetCode2.Common.Storages;
using NetCode2.Server.Realtime.Runtime.ServerData;
using NetCode2.Server.Realtime.Runtime.Storages.Contracts;

namespace NetCode2.Server.Realtime.Runtime.Storages
{
    public class RoomInMemoryStorage : InMemoryStorage<RoomId,RoomServerData>, IRoomStorage
    {
    }
}