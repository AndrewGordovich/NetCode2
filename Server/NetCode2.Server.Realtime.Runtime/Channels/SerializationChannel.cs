using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class SerializationChannel : MessageChannel<IPlayerMessage>, ISerializationChannel
    {
        public SerializationChannel(IChannelHandler channelHandler)
            : base(channelHandler, true, false)
        {
        }
    }
}