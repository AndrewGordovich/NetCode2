using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferReader : BitBuffer
    {
        public BitBufferReader(int capacity)
        {
            Chunks = new uint[capacity];
            Reset();
        }

        public void CopyFrom(ReadOnlySpan<byte> data)
        {
            var length = data.Length;
            Reset();
            var step = Unsafe.SizeOf<UInt32>();
            Int32 numChunks = (length / step) + 1;

            if(Chunks.Length < numChunks)
            {
                Chunks = new UInt32[numChunks];
            }

            for (var i = 0; i < numChunks; i++)
            {
                Int32 dataIdx = i * step;
                UInt32 chunk = 0;

                if (dataIdx < length)
                    chunk = (UInt32)data[dataIdx];

                if (dataIdx + 1 < length)
                    chunk = chunk | (UInt32)data[dataIdx + 1] << 8;

                if (dataIdx + 2 < length)
                    chunk = chunk | (UInt32)data[dataIdx + 2] << 16;

                if (dataIdx + 3 < length)
                    chunk = chunk | (UInt32)data[dataIdx + 3] << 24;

                Chunks[i] = chunk;
            }
        }
    }
}