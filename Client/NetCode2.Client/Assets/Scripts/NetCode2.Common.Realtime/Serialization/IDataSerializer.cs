using System;

namespace NetCode2.Common.Realtime.Serialization
{
    public interface IDataSerializer<T>
    {
        byte[] Serialize(T data);

        T Deserialize(Span<byte> span);
    }

    public interface IDataSerializer
    {
        byte[] Serialize(object data);

        object Deserialize(Span<byte> span);
    }
}