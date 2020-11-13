namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitInStream
    {
        public byte ReadByte() => bitBuffer.ReadByte();

        public ushort ReadShort() => bitBuffer.ReadShort();
    }
}