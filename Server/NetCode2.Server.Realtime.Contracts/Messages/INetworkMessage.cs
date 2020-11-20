using System;

namespace NetCode2.Server.Realtime.Contracts.Messages
{
    public interface INetworkMessage
    {
        PeerId PeerId { get; }

        Span<byte> Span { get; }

        NetworkMessageType MessageType { get; }

        int Length { get; }

        void Dispose();
    }
}