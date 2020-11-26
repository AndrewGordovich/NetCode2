namespace NetCode2.Server.Realtime.Contracts.Channels
{
    public interface ISimpleMessageChannel<TMessage>
    {
        bool TryRead(out TMessage message);

        bool TryWrite(in TMessage message);
    }
}