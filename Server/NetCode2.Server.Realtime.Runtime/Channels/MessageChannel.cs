using System;
using System.Threading;
using System.Threading.Channels;
using NetCode2.Server.Realtime.Contracts.Channels;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public abstract class MessageChannel<TMessage> : IMessageChannel<TMessage>
    {
        protected IChannelHandler ChannelHandler { get; }

        protected Channel<TMessage> Channel { get; }

        public MessageChannel(IChannelHandler channelHandler, bool singleReader, bool singleWriter)
        {
            ChannelHandler = channelHandler;
            Channel = System.Threading.Channels.Channel.CreateUnbounded<TMessage>(new UnboundedChannelOptions
                {SingleReader = singleReader, SingleWriter = singleWriter});

        }

        public void StartProcessing(Action<TMessage> processor, CancellationToken cancellationToken = default) =>
            ChannelHandler.StartProcessingMessages(Channel, processor, cancellationToken);


        public void StopProcessing() =>
            ChannelHandler.StopProcessingMessages(Channel);

        public bool TryWrite(in TMessage message)
        {
            if (Channel.Writer.TryWrite(message))
            {
                return true;
            }

            return false;
        }
    }
}