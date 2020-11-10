using System;

namespace NetCode2.Server.Realtime.Contracts.Messages
{
    public interface INetworkMessage
    {
        Span<byte> Span { get; }
    }
}