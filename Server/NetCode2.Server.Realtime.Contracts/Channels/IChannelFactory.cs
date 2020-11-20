namespace NetCode2.Server.Realtime.Contracts.Channels
{
    public interface IChannelFactory
    {
        IRoomChannel CreateRoomChannel();
    }
}