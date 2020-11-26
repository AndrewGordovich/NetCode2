using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public interface IRoomPlayerDictionary
    {
        void Add(RoomPlayer player);

        void Remove(ClientId clientId);

        bool TryGetValue(ClientId clientId, out RoomPlayer roomPlayer);

        RoomPlayer GetPlayer(ClientId clientId);

        int Count { get; }

        void Clear();
    }
}