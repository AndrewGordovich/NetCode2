using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Common
{
    public readonly struct RoomId : IEquatable<RoomId>
    {
        private readonly Guid value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RoomId(Guid value)
        {
            if (Guid.Empty.Equals(value))
            {
                Throw.InvalidOperation("Must be non empty id");
            }

            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RoomId(Guid value) => new RoomId(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Guid(RoomId roomId) => roomId.value;

        public bool Equals(RoomId other) => value.Equals(other.value);
    }
}