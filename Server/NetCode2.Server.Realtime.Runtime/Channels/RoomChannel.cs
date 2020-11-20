using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class RoomChannel : MessageChannel<IRoomMessage>, IRoomChannel
    {
        public RoomChannel(IChannelHandler channelHandler)
            : base(channelHandler, true, false)
        {
        }
    }
}