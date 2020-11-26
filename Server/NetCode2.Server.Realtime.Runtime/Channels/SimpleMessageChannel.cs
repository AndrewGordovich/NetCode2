using System.Threading.Channels;
using NetCode2.Server.Realtime.Contracts.Channels;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class SimpleMessageChannel<TMessage> : ISimpleMessageChannel<TMessage>
    {
        private readonly Channel<TMessage> channel;

        public SimpleMessageChannel(bool singleReader, bool singleWriter)
        {
            channel = Channel.CreateUnbounded<TMessage>(new UnboundedChannelOptions{SingleReader = singleReader, SingleWriter = singleWriter});
        }

        public bool TryRead(out TMessage message)
        {
            if (channel.Reader.TryRead(out message))
            {
                return true;
            }

            return false;
        }

        public bool TryWrite(in TMessage message)
        {
            if (channel.Writer.TryWrite(message))
            {
                return true;
            }

            return false;
        }
    }
}