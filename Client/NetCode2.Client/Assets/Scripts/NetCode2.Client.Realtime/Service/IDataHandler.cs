namespace NetCode2.Client.Realtime.Service
{
    public interface IDataHandler
    {
        void Handle(INetworkData data);
    }
}