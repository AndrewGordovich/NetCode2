using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Contracts.Channels
{
    public interface IRoomChannel : IMessageChannel<IRoomMessage>
    {
    }
}