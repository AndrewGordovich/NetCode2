using System;

namespace NetCode2.Common.Realtime.Serialization
{
    public abstract class DataSerializer<T> : IDataSerializer<T> , IDataSerializer
    {
        public abstract byte[] Serialize(T data);

        byte[] IDataSerializer.Serialize(object data) => Serialize((T) data);

        public abstract T Deserialize(Span<byte> span);

        object IDataSerializer.Deserialize(Span<byte> span) => Deserialize(span);
    }
}