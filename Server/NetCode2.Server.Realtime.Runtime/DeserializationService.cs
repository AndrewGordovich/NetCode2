using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetCode2.Common.Realtime.Serialization;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.Runtime
{
    public class DeserializationService<TNetworkMessage> : IDeserializationService
        where TNetworkMessage : INetworkMessage
    {
        private readonly IDeserializationChannel<TNetworkMessage> deserializationChannel;
        private readonly IClientManager clientManager;
        private readonly ILogger<DeserializationService<TNetworkMessage>> logger;

        private readonly SimulationCommandsSerializer commandsSerializer;
        private readonly IChannelFactory channelFactory;

        private CancellationTokenSource cancellationTokenSource;
        private RoomEngine.Gameplay.RoomEngine roomEngine;

        public DeserializationService(
            IDeserializationChannel<TNetworkMessage> deserializationChannel,
            IClientManager clientManager,
            ILogger<DeserializationService<TNetworkMessage>> logger,
            SimulationCommandsSerializer commandsSerializer,
            IChannelFactory channelFactory)
        {
            this.deserializationChannel = deserializationChannel;
            this.clientManager = clientManager;
            this.logger = logger;
            this.commandsSerializer = commandsSerializer;
            this.channelFactory = channelFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            deserializationChannel.StartProcessing(message => Processor(in message), cancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            deserializationChannel.StopProcessing();
            cancellationTokenSource?.Cancel();

            return Task.CompletedTask;
        }

        private void Processor(in TNetworkMessage message)
        {
            try
            {
                switch (message.MessageType)
                {
                    case NetworkMessageType.Receive:
                        ReceiveMessage(in message);
                        message.Dispose();
                        break;
                    case NetworkMessageType.Connect:
                        clientManager.Connect(message.PeerId);

                        roomEngine = new RoomEngine.Gameplay.RoomEngine(channelFactory, logger);
                        roomEngine.StartGame();
                        break;
                    case NetworkMessageType.Disconnect:
                        clientManager.Disconnect(message.PeerId);
                        break;
                    case NetworkMessageType.Timeout:
                        clientManager.ClientTimeout(message.PeerId);
                        break;

                    default:
                        throw new NotImplementedException($"Message type {message.MessageType} is not supported.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $@"Failed to process the message of type {message.MessageType}.
                                    The peer {message.PeerId} will be disconnected.");
                throw;
            }
        }

        private void ReceiveMessage(in TNetworkMessage message)
        {
            if (clientManager.TryGetClient(message.PeerId, out var client))
            {
                var commands = commandsSerializer.Deserialize(message.Span);
            }
            else
            {
                logger.LogError("Client {id} not found", message.PeerId);
            }
        }
    }
}