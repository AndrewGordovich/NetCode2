using System;
using System.Runtime.CompilerServices;

namespace NetCode2.Server.Realtime.Contracts
{
    /// <summary>
    /// Client identifier. Equivalent of <see cref="PeerId"/>.
    /// </summary>
    public readonly struct ClientId : IEquatable<ClientId>
    {
        private readonly uint value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ClientId(uint value)
        {
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ClientId(uint value) => new ClientId(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(ClientId clientId) => clientId.value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ClientId(PeerId peerId) => new ClientId(peerId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PeerId(ClientId clientId) => new PeerId(clientId.value);

        public bool Equals(ClientId other) => value == other.value;
    }
}