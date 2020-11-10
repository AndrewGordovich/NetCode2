using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Contracts.Channels
{
    public interface IDeserializationChannel<TMessage> : IMessageChannel<TMessage>
    where TMessage : INetworkMessage
    {
    }
}