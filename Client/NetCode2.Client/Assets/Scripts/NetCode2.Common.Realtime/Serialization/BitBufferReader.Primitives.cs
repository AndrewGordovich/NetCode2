using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferReader
    {
        [MethodImpl(256)]
        public byte ReadByte() => (byte) Read(8);

        [MethodImpl(256)]
        public ushort ReadShort() => (ushort) Read(16);
    }
}