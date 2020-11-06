using System.Runtime.CompilerServices;

namespace NetCode2.Common.Realtime.Serialization
{
    public partial class BitBufferWriter
    {
        [MethodImpl(256)]
        public void AddByte(byte value) => Add(value, 8);

        [MethodImpl(256)]
        public void AddShort(ushort value) => Add(value, 16);
    }
}