using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferWriter
    {
        [MethodImpl(256)]
        private void Add(uint value, int numberOfBits)
        {
            value &= (UInt32)((1ul << numberOfBits) - 1);

            Scratch |= ((UInt64) value) << ScratchUsedBits;
            ScratchUsedBits += numberOfBits;

            if (ScratchUsedBits >= 32)
            {
                Chunks[ChunkIndex] = (UInt32) (Scratch);
                Scratch >>= 32;
                ScratchUsedBits -= 32;
                ChunkIndex++;
            }
        }

    }
}