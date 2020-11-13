using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class DeserializationChannel<TMessage> : MessageChannel<TMessage>, IDeserializationChannel<TMessage>
    where TMessage : INetworkMessage
    {
        public DeserializationChannel(IChannelHandler channelHandler)
            : base(channelHandler, true, true)
        {
        }
    }
}