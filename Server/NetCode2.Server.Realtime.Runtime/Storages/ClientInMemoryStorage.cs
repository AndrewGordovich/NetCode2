using NetCode2.Common.Storages;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Storages;

namespace NetCode2.Server.Realtime.Runtime.Storages
{
    public class ClientInMemoryStorage : InMemoryStorage<ClientId, IClient>, IClientStorage
    {
    }
}