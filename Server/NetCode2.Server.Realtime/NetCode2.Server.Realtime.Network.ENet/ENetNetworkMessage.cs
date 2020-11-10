using System;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public class ENetNetworkMessage : INetworkMessage
    {
        public Span<byte> Span
        {
            get
            {
                unsafe
                {
                    var span = new Span<byte>(packet)
                }
            }
    }

        public ENetNetworkMessage(Packet)
        {

        }
    }
}