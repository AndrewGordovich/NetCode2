using NetCode2.Common;
using NetCode2.Common.Storages.Contracts;
using NetCode2.Server.Realtime.Runtime.ServerData;

namespace NetCode2.Server.Realtime.Runtime.Storages.Contracts
{
    public interface IRoomStorage : IStorage<RoomId, RoomServerData>
    {
    }
}