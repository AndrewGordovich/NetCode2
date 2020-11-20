using NetCode2.Common.Storages.Contracts;

namespace NetCode2.Server.Realtime.Contracts.Storages
{
    public interface IPeerStorage : IStorage<PeerId, IPeer>
    {
        bool TryGetPeer(PeerId peerId, out IPeer peer);
    }
}