using System;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitInStream
    {
        private readonly BitBufferReader bitBuffer;

        public BitInStream(int capacity)
        {
            bitBuffer = new BitBufferReader(capacity);
        }

        public void FromSpan(in Span<byte> span)
        {
            bitBuffer.CopyFrom(span);
        }
    }
}