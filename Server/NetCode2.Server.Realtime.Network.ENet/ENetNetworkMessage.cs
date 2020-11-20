using System;
using ENet;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public readonly struct ENetNetworkMessage : INetworkMessage
    {
        private readonly Packet? packet;

        public PeerId PeerId { get; }

        public int Length { get; }

        public NetworkMessageType MessageType { get; }

        public Span<byte> Span
        {
            get
            {
                unsafe
                {
                    var span = new Span<byte>(packet.GetValueOrDefault().Data.ToPointer(), Length);
                    return span;
                }
            }
        }

        public ENetNetworkMessage(PeerId peerId, Packet packet, int length)
        {
            PeerId = peerId;
            this.packet = packet;
            Length = length;
            MessageType = NetworkMessageType.Receive;
        }

        public ENetNetworkMessage(PeerId peerId, NetworkMessageType messageType)
        {
            PeerId = peerId;
            packet = null;
            Length = 0;
            MessageType = messageType;
        }

        public void Dispose()
        {
            packet.GetValueOrDefault().Dispose();
        }
    }
}