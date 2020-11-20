namespace NetCode2.Server.Realtime.Contracts.Messages
{
    public enum NetworkMessageType : byte
    {
        Connect = 1,
        Disconnect = 2,
        Receive = 3,
        Timeout = 4,
    }
}