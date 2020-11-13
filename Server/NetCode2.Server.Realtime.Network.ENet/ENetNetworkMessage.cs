using System;
using ENet;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public readonly struct ENetNetworkMessage : INetworkMessage
    {
        private readonly Packet? packet;

        public int Length { get; }

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

        public ENetNetworkMessage(Packet packet, int length)
        {
            this.packet = packet;
            Length = length;
        }

        public void Dispose()
        {
            packet.GetValueOrDefault().Dispose();
        }
    }
}