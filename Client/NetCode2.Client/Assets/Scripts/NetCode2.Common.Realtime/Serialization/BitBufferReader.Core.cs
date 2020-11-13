using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferReader
    {
        [MethodImpl(256)]
        private UInt32 Read(Int32 numberOfBits)
        {
            if(ScratchUsedBits < numberOfBits)
            {
                Scratch |= ((UInt64)(Chunks[ChunkIndex])) << ScratchUsedBits;
                ScratchUsedBits += 32;
                ChunkIndex++;
            }

            UInt32 output = (UInt32)(Scratch & ((((UInt64)1) << numberOfBits) - 1));

            Scratch >>= numberOfBits;
            ScratchUsedBits -= numberOfBits;

            return output;
        }
    }
}