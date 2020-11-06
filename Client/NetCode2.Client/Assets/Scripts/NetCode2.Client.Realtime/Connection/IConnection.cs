namespace NetCode2.Client.Realtime.Connection
{
    public interface IConnection
    {
        ConnectionState ConnectionState { get; }

        void Connect();
    }
}