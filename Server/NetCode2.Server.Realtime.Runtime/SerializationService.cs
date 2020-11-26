using System;
using System.Threading;
using System.Threading.Tasks;
using NetCode2.Common.Realtime.Data.Events;
using NetCode2.Common.Realtime.Serialization;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;
using NetCode2.Server.Realtime.RoomEngine.Channels.PlayerMessages;

namespace NetCode2.Server.Realtime.Runtime
{
    public class SerializationService : ISerializationService
    {
        private readonly ISerializationChannel serializationChannel;
        private readonly INetworkServerChannel networkServerChannel;
        private readonly GameJoinedEventDataSerializer gameJoinedDataSerializer;

        private CancellationTokenSource cancellationTokenSource;

        public SerializationService(
            ISerializationChannel serializationChannel,
            INetworkServerChannel networkServerChannel,
            GameJoinedEventDataSerializer gameJoinedDataSerializer)
        {
            this.serializationChannel = serializationChannel;
            this.networkServerChannel = networkServerChannel;
            this.gameJoinedDataSerializer = gameJoinedDataSerializer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            serializationChannel.StartProcessing(Processor, cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            serializationChannel.StopProcessing();
            cancellationTokenSource?.Cancel();
            return Task.CompletedTask;
        }

        private void Processor(IPlayerMessage message)
        {
            switch (message)
            {
                case GameJoinedMessage gameJoinedMessage:
                    Process(gameJoinedMessage);
                    break;

                default:
                    throw new NotImplementedException($"Processing message of type {message.GetType().Name} is not supported");
            }
        }

        private void Process(GameJoinedMessage gameJoinedMessage)
        {
            var gameJoinedData = new GameJoinedEventData();
            var data = gameJoinedDataSerializer.Serialize(gameJoinedData);

            var networkMessage = new NetworkMessage(gameJoinedMessage.ClientId, data, data.Length, true);
            networkServerChannel.TryWrite(in networkMessage);
        }
    }
}