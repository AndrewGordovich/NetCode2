using System;
using System.Threading;
using System.Threading.Tasks;
using ENet;
using Microsoft.Extensions.Logging;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;
using NetCode2.Server.Realtime.Contracts.Storages;

namespace NetCode2.Server.Realtime.Network.ENet
{
    public class ENetServer : INetworkServer
    {
        public const byte IngoingUnrelibleChannelId = 0;
        public const byte IngoingRelibleChannelId = 2;
        public const byte OutgoingUnrelibleChannelId = 1;
        public const byte OutgoingRelibleChannelId = 3;
        public const byte ChannelsCount = 4;

        private readonly IPeerStorage peerStorage;
        private readonly IDeserializationChannel<ENetNetworkMessage> deserializationChannel;
        private readonly INetworkServerChannel networkServerChannel;
        private readonly ILogger<ENetServer> logger;

        private Host server;
        private CancellationTokenSource cancellationTokenSource;

        public ENetServer(
            IPeerStorage peerStorage,
            IDeserializationChannel<ENetNetworkMessage> deserializationChannel,
            INetworkServerChannel networkServerChannel,
            ILogger<ENetServer> logger)
        {
            this.peerStorage = peerStorage;
            this.deserializationChannel = deserializationChannel;
            this.networkServerChannel = networkServerChannel;
            this.logger = logger;
        }

        public Task Start(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            server = new Host();
            Address address = default;
            address.Port = 40002;
            server.Create(address, 10, 4);

            Task.Factory.StartNew(() => RunLoop(server, cancellationTokenSource.Token),
                TaskCreationOptions.LongRunning);

            logger.LogInformation("ENet server started");
            return Task.CompletedTask;
        }

        private void RunLoop(Host host, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    ReadAllFromServerChannel();
                    server.Service(15, out Event netEvent);

                    Process(ref netEvent);
                }
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        private void ReadAllFromServerChannel()
        {
            while(networkServerChannel.TryRead(out var message))
            {
                CreateAndSendPacket(in message);
            }
        }

        private void CreateAndSendPacket(in NetworkMessage message)
        {
            if (peerStorage.TryGetPeer(message.PeerId, out var peer))
            {
                if (message.IsReliable)
                {
                    peer.SendReliable(message.Data, message.Length);
                }
                else
                {
                    peer.SendUnreliable(message.Data, message.Length);
                }
            }
            else
            {
                // do nothing
            }
        }

        private void Process(ref Event networkEvent)
        {
            switch (networkEvent.Type)
            {
                case EventType.None:
                    break;

                case EventType.Connect:
                    logger.LogInformation("Client connected - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Connect Event received from client - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);

                    peerStorage.Add(networkEvent.Peer.ID, new ENetPeerWrapper(networkEvent.Peer));
                    var connectMessage = new ENetNetworkMessage(networkEvent.Peer.ID, NetworkMessageType.Connect);
                    WriteToDeserializationChannel(in connectMessage);
                    break;

                case EventType.Disconnect:
                    logger.LogInformation("Client disconnected - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Disconnect Event received from client - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);

                    peerStorage.Remove(networkEvent.Peer.ID);

                    var disconnectMessage = new ENetNetworkMessage(networkEvent.Peer.ID, NetworkMessageType.Disconnect);
                    WriteToDeserializationChannel(in disconnectMessage);
                    break;

                case EventType.Timeout:
                    logger.LogInformation("Client timeout - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Client timeout - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);

                    peerStorage.Remove(networkEvent.Peer.ID);
                    var timeoutMessage = new ENetNetworkMessage(networkEvent.Peer.ID, NetworkMessageType.Timeout);
                    WriteToDeserializationChannel(timeoutMessage);
                    break;

                case EventType.Receive:
                    Console.WriteLine("Packet received from - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP + ", Channel ID: " + networkEvent.ChannelID + ", Data length: " + networkEvent.Packet.Length);

                    var receivedMessage = new ENetNetworkMessage(networkEvent.Peer.ID, networkEvent.Packet, networkEvent.Packet.Length);
                    WriteToDeserializationChannel(in receivedMessage);
                    break;

                default:
                    throw new NotImplementedException($"The event type {networkEvent.Type} is not supported.");
            }
        }

        private void WriteToDeserializationChannel(in ENetNetworkMessage message)
        {
            if (!deserializationChannel.TryWrite(in message))
            {
                logger.LogError("Message can't be written to the channel.");
            }
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            cancellationTokenSource?.Cancel();

            server?.Dispose();
            server = null;

            return Task.CompletedTask;
        }
    }
}