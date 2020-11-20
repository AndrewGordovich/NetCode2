using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Server.Realtime.Contracts
{
    public readonly struct PeerId : IEquatable<PeerId>
    {
        private readonly uint value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PeerId(uint value)
        {
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PeerId(uint value) => new PeerId(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(PeerId clientId) => clientId.value;

        public bool Equals(PeerId other) => value == other.value;
    }
}