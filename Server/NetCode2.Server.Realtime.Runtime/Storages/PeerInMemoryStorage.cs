using NetCode2.Common.Storages;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Storages;

namespace NetCode2.Server.Realtime.Runtime.Storages
{
    public class PeerInMemoryStorage : InMemoryStorage<PeerId, IPeer>, IPeerStorage
    {
        public bool TryGetPeer(PeerId peerId, out IPeer peer) => TryGet(peerId, out peer);
    }
}