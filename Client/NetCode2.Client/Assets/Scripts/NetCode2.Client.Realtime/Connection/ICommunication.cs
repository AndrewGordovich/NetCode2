using NetCode2.Client.Realtime.Service;

namespace NetCode2.Client.Realtime.Connection
{
    public interface ICommunication
    {
        void ServiceAll();
        void ServiceOnce();
        void SendReliable(byte[] data, int length);
        bool HasData();
        INetworkData GetData();
    }
}