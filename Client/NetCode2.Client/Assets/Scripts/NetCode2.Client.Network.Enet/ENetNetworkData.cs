using System;
using ENet;
using NetCode2.Client.Realtime.Service;
using NetCode2.Common.Realtime.Service;

namespace NetCode2.Client.Network.Enet
{
    public sealed class ENetNetworkData : INetworkData
    {
        public ENetNetworkData(Packet? packet, int length)
        {
            this.packet = packet;
            Length = length;
        }

        private readonly Packet? packet;

        private NetworkDataCode? code;

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

        public NetworkDataCode Code
        {
            get
            {
                if (code.HasValue)
                {
                    return code.Value;
                }

                code = (NetworkDataCode) Span[0];
                return code.Value;
            }
        }

        public int Length { get; }

        public void Dispose()
        {
            packet.GetValueOrDefault().Dispose();
        }
    }
}