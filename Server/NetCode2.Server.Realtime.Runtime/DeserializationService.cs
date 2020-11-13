using System.Threading;
using System.Threading.Tasks;
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
        private readonly SimulationCommandsSerializer commandsSerializer;

        private CancellationTokenSource cancellationTokenSource;

        public DeserializationService(
            IDeserializationChannel<TNetworkMessage> deserializationChannel,
            SimulationCommandsSerializer commandsSerializer)
        {
            this.deserializationChannel = deserializationChannel;
            this.commandsSerializer = commandsSerializer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            deserializationChannel.StartProcessing(message => ProcessMessage(in message), cancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            deserializationChannel.StopProcessing();
            cancellationTokenSource?.Cancel();

            return Task.CompletedTask;
        }

        private void ProcessMessage(in TNetworkMessage message)
        {
            var commands = commandsSerializer.Deserialize(message.Span);
        }
    }
}