namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitOutStream
    {
        public void WriteByte(byte value) => bitBuffer.AddByte(value);

        public void WriteShort(ushort value) => bitBuffer.AddShort(value);
    }
}