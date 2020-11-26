using System;
using NetCode2.Common.Realtime.Data.Events;
using NetCode2.Common.Realtime.Serialization;

namespace NetCode2.Client.Realtime.Service
{
    public class GameJoinedDataHandler : IDataHandler
    {
        private readonly DataSerializer<GameJoinedEventData> serializer = new GameJoinedEventDataSerializer();

        public event Action<GameJoinedEventData> GameJoined;

        public void Handle(INetworkData data)
        {
            GameJoined?.Invoke(serializer.Deserialize(data.Span));
        }
    }
}