namespace NetCode2.Client.Realtime.Connection
{
    public interface ICommunication
    {
        void ServiceOnce();
        void SendReliable(byte[] data, int length);
    }
}