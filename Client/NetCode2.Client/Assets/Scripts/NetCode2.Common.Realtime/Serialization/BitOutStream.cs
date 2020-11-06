namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitOutStream
    {
        private readonly BitBufferWriter bitBuffer;

        public BitOutStream(int capacity)
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