using System;
using System.Runtime.Serialization;
using NetCode2.Common.Realtime.Data.Events;
using NetCode2.Common.Realtime.Service;
using NetCode2.Common.Utils;

namespace NetCode2.Common.Realtime.Serialization
{
    public class GameJoinedEventDataSerializer : DataSerializer<GameJoinedEventData>
    {
        private const NetworkDataCode NetworkSerializerDataCode = NetworkDataCode.GameJoined;

        public override byte[] Serialize(GameJoinedEventData data)
        {
            var outStream = new BitOutStream(ushort.MaxValue);

            outStream.WriteByte((byte)NetworkSerializerDataCode);

            return outStream.ToArray();
        }

        public override GameJoinedEventData Deserialize(Span<byte> span)
        {
            var inStream = new BitInStream(span);

            var networkDataCode = (NetworkDataCode) inStream.ReadByte();
            Contract.Requires<SerializationException>(networkDataCode == NetworkSerializerDataCode, "Wrong {0}: {1}.", nameof(NetworkDataCode), networkDataCode);

            return new GameJoinedEventData();
        }
    }
}