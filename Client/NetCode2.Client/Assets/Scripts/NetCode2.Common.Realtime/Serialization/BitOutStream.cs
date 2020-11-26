namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitOutStream
    {
        private const int DefaultCapacity = 375;

        private readonly BitBufferWriter bitBuffer;

        public BitOutStream(int capacity = DefaultCapacity)
        {
            bitBuffer = new BitBufferWriter(capacity);
        }

        public byte[] ToArray()
        {
            var result = bitBuffer.ToArray();
            bitBuffer.Reset();
            return result;
        }
    }
}