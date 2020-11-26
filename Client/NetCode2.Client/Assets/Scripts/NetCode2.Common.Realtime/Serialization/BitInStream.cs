using System;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitInStream
    {
        public const int DefaultCapacity = 375;

        private readonly BitBufferReader bitBuffer;

        public BitInStream(int capacity = DefaultCapacity)
        {
            bitBuffer = new BitBufferReader(capacity);
        }

        public BitInStream(Span<byte> span)
        {
            bitBuffer = new BitBufferReader();
            bitBuffer.CopyFrom(span);
        }

        public void FromSpan(in Span<byte> span)
        {
            bitBuffer.CopyFrom(span);
        }
    }
}