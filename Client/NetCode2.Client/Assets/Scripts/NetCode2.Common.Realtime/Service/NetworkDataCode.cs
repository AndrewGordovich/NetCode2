namespace NetCode2.Common.Realtime.Service
{
    public enum NetworkDataCode : byte
    {
        // data sent from server to client
        GameJoined,

        // commands sent from client to server
        JoinGameCommand,
    }
}