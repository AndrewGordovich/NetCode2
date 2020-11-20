using ENet;
using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public class ENetPeerWrapper : IPeer
    {
        private readonly Peer peer;

        public PeerId Id => peer.ID;

        public ENetPeerWrapper(Peer peer)
        {
            this.peer = peer;
        }

        public void SendUnreliable(byte[] data, int length)
        {
            Send(data, length, ENetServer.OutgoingUnrelibleChannelId, PacketFlags.None | PacketFlags.Unsequenced);
        }

        public void SendReliable(byte[] data, int length)
        {
            Send(data, length, ENetServer.OutgoingRelibleChannelId, PacketFlags.Reliable);
        }

        public void Disconnect() => peer.Disconnect(0);

        private void Send(byte[] data, int length, byte channelId, PacketFlags flags)
        {
            Packet packet = default;

            packet.Create(data, length, flags);
            peer.Send(channelId, ref packet);
        }
    }
}