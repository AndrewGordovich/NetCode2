using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetCode2.Common;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Serialization;
using NetCode2.Common.Realtime.Service;
using NetCode2.Server.Common.Meta.Communication;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;
using NetCode2.Server.Realtime.RoomEngine.Channels.RoomMessages;
using NetCode2.Server.Realtime.Runtime.ServerData;
using NetCode2.Server.Realtime.Runtime.Storages.Contracts;

namespace NetCode2.Server.Realtime.Runtime
{
    public class DeserializationService<TNetworkMessage> : IDeserializationService
        where TNetworkMessage : INetworkMessage
    {
        private readonly IDeserializationChannel<TNetworkMessage> deserializationChannel;
        private readonly IClientManager clientManager;
        private readonly ILogger<DeserializationService<TNetworkMessage>> logger;

        private readonly SimulationCommandsSerializer commandsSerializer;
        private readonly IDataSerializer<JoinGameCommandData> joinGameSerializer;
        private readonly IChannelFactory channelFactory;
        private readonly IRoomStorage roomStorage;

        private CancellationTokenSource cancellationTokenSource;
        private RoomEngine.Gameplay.RoomEngine roomEngine;

        public DeserializationService(
            IDeserializationChannel<TNetworkMessage> deserializationChannel,
            IClientManager clientManager,
            ILogger<DeserializationService<TNetworkMessage>> logger,
            SimulationCommandsSerializer commandsSerializer,
            IChannelFactory channelFactory,
            IRoomStorage roomStorage,
            IDataSerializer<JoinGameCommandData> joinGameSerializer)
        {
            this.deserializationChannel = deserializationChannel;
            this.clientManager = clientManager;
            this.logger = logger;
            this.commandsSerializer = commandsSerializer;
            this.channelFactory = channelFactory;
            this.roomStorage = roomStorage;
            this.joinGameSerializer = joinGameSerializer;
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
                logger.LogError(ex, $@"Failed to process the message of type {message.MessageType}. The peer {message.PeerId} will be disconnected.
                    {ex}");
                throw;
            }
        }

        private void ReceiveMessage(in TNetworkMessage message)
        {
            if (clientManager.TryGetClient(message.PeerId, out var client))
            {
                ReceiveMessage(client, message.Span);
            }
            else
            {
                logger.LogError("Client {id} not found", message.PeerId);
            }
        }

        private void ReceiveMessage(IClient client, Span<byte> span)
        {
            var networkDataCode = (NetworkDataCode) span[0];
            switch (networkDataCode)
            {
                case NetworkDataCode.JoinGameCommand:
                    HandleJoinGameCommand(client, span);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleJoinGameCommand(IClient client, in Span<byte> span)
        {
            var commandData = joinGameSerializer.Deserialize(span);

            RoomServerData roomData = FindNotFullRoom();
            if (roomData == null)
            {
                roomData = CreateRoomAndStartMatch();
                roomStorage.Add(roomData.RoomMetaData.RoomId, roomData);
            }

            roomData.RoomEngine.SendMessage(new JoinRoomMessage(client));
        }

        private RoomServerData FindNotFullRoom()
        {
            using var roomEnumerator = roomStorage.GetEnumerator();

            while (roomEnumerator.MoveNext())
            {
                var roomData = roomEnumerator.Current.Value;

                if (roomData.RoomMetaData.Players.Count < 2)
                {
                    return roomData;
                }
            }

            return null;
        }

        private RoomServerData CreateRoomAndStartMatch()
        {
            RoomId roomId = Guid.NewGuid();

            var roomMetaData = new RoomMetaData()
            {
                RoomId = roomId
            };

            roomEngine = new RoomEngine.Gameplay.RoomEngine(roomId, channelFactory, logger);
            roomEngine.StartGame();

            var data = new RoomServerData
            {
                RoomEngine = roomEngine,
                RoomMetaData = roomMetaData
            };
            return data;
        }
    }
}