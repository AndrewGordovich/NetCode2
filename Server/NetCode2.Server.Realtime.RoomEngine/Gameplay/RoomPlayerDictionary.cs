using System.Collections.Generic;
using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public class RoomPlayerDictionary : IRoomPlayerDictionary
    {
        private readonly IDictionary<ClientId, RoomPlayer> players;

        public RoomPlayerDictionary()
        {
            players = new Dictionary<ClientId, RoomPlayer>();
        }

        public void Add(RoomPlayer player)
        {
            players.Add(player.ClientId, player);
        }

        public void Remove(ClientId clientId)
        {
            var player = GetPlayer(clientId);
            players.Remove(clientId);
        }

        public bool TryGetValue(ClientId clientId, out RoomPlayer roomPlayer)
        {
            return players.TryGetValue(clientId, out roomPlayer);
        }

        public RoomPlayer GetPlayer(ClientId clientId)
        {
            return players[clientId];
        }

        public int Count => players.Count;

        public void Clear()
        {
            players.Clear();
        }
    }
}