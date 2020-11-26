using System;

namespace NetCode2.Common.Realtime.Serialization
{
    public abstract class BitBuffer
    {
        public const int DefaultCapacity = 375;

        protected Int32 ScratchUsedBits { get; set; }
        protected UInt64 Scratch { get; set; }
        protected uint[] Chunks { get; set; }
        protected Int32 ChunkIndex { get; set; }

        public void Reset()
        {
            ChunkIndex = 0;
            Scratch = 0;
            ScratchUsedBits = 0;
        }

        public void Align()
        {
            if (ScratchUsedBits != 0)
            {
                Chunks[ChunkIndex] = (UInt32) (Scratch & 0xFFFFFFFF);
                Scratch >>= 32;
                ScratchUsedBits -= 32;
                ChunkIndex++;
            }
        }
    }
}