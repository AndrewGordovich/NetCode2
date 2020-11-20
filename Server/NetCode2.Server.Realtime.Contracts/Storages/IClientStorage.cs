using NetCode2.Common.Storages.Contracts;

namespace NetCode2.Server.Realtime.Contracts.Storages
{
    public interface IClientStorage : IStorage<ClientId, IClient>
    {
    }
}