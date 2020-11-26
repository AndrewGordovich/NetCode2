using System;
using NetCode2.Common.Realtime.Data.Commands;

namespace NetCode2.Common.Realtime.Serialization
{
    public class SimulationCommandsSerializer
    {
        private const ushort StreamCapacity = 375;

        private readonly BitOutStream outStream = new BitOutStream(StreamCapacity);
        private readonly BitInStream inStream = new BitInStream(StreamCapacity);

        public byte[] Serialize(SimulationCommand command)
        {
            outStream.WriteByte(155);
            outStream.WriteShort(60000);
            outStream.WriteShort(60000);

            return outStream.ToArray();
        }

        public byte[] Deserialize(Span<byte> span)
        {
            inStream.FromSpan(span);

            var byteData = inStream.ReadByte();
            Console.WriteLine($"byteData: {byteData}");

            var shortData = inStream.ReadShort();
            Console.WriteLine($"shortData: {shortData}");

            var shortSecondData = inStream.ReadShort();
            Console.WriteLine($"shortSecondData: {shortSecondData}");

            return new byte[0];
        }
    }
}