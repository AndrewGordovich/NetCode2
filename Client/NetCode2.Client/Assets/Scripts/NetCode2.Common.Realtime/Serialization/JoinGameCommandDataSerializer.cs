using System;
using System.Runtime.Serialization;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Service;
using NetCode2.Common.Utils;

namespace NetCode2.Common.Realtime.Serialization
{
    public class JoinGameCommandDataSerializer : DataSerializer<JoinGameCommandData>
    {
        private const NetworkDataCode NetworkSerializerCode = NetworkDataCode.JoinGameCommand;

        public override byte[] Serialize(JoinGameCommandData data)
        {
            var outStream = new BitOutStream();

            outStream.WriteByte((byte)NetworkSerializerCode);

            return outStream.ToArray();
        }

        public override JoinGameCommandData Deserialize(Span<byte> span)
        {
            var inStream = new BitInStream(span);

            var networkDataCode = (NetworkDataCode) inStream.ReadByte();
            Contract.Requires<SerializationException>(networkDataCode == NetworkSerializerCode, "Wrong {0}: {1}.", nameof(NetworkDataCode), networkDataCode);

            return new JoinGameCommandData();
        }
    }
}