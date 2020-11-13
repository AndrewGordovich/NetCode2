using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferWriter : BitBuffer
    {
        private Int32 LengthWritten => ((BitsWritten - 1) >> 3) + 1;

        protected Int32 BitsWritten
        {
            get
            {
                var indexInBits = ChunkIndex * 32;
                var over = ScratchUsedBits != 0 ? 1 : 0;
                return indexInBits + over * Math.Abs(ScratchUsedBits);
            }
        }

        public BitBufferWriter(int capacity)
        {
            Chunks = new uint[capacity];
        }

        public byte[] ToArray()
        {
            var data = new byte[LengthWritten];
            ToSpan(data);
            return data;
        }

        private void ToSpan(Span<byte> data)
        {
            Add(1, 1);
            var bitsPassed = BitsWritten;
            Align();

            Int32 numChunks = (bitsPassed >> 5) + 1;
            Int32 length = data.Length;
            var step = Unsafe.SizeOf<UInt32>();
            for (var i = 0; i < numChunks; i++)
            {
                Int32 dataIdx = i * step;
                UInt32 chunk = Chunks[i];
                // TODO: optimize by copying 4 byte in single call via Unsafe
                if (dataIdx < length)
                    data[dataIdx] = (byte) (chunk);

                if (dataIdx + 1 < length)
                    data[dataIdx + 1] = (byte) (chunk >> 8);

                if (dataIdx + 2 < length)
                    data[dataIdx + 2] = (byte) (chunk >> 16);

                if (dataIdx + 3 < length)
                    data[dataIdx + 3] = (byte) (chunk >> 24);
            }
        }
    }
}