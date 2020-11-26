using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class NetworkServerChannel : SimpleMessageChannel<NetworkMessage>, INetworkServerChannel
    {
        public NetworkServerChannel() :
            base(true, true)
        {
        }
    }
}