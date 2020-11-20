namespace NetCode2.Server.Realtime.Contracts
{
    public interface IPeer
    {
        PeerId Id { get; }

        void SendUnreliable(byte[] data, int length);

        void SendReliable(byte[] date, int length);

        void Disconnect();
    }
}