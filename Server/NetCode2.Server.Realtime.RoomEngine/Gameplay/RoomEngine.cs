using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Collections.Pooled;
using Microsoft.Extensions.Logging;
using NetCode2.Common;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;
using NetCode2.Server.Realtime.RoomEngine.Channels.PlayerMessages;
using NetCode2.Server.Realtime.RoomEngine.Channels.RoomMessages;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public sealed class RoomEngine
    {
        private readonly RoomId roomId;

        private readonly ILogger logger;
        private readonly IRoomChannel roomChannel;

        private readonly CancellationTokenSource gameLoopCancellationTokenSource;
        private readonly CancellationTokenSource processingCancellationTokenSource;
        private readonly PooledList<IRoomServiceMessage> serviceMessages;

        private readonly IRoomPlayerDictionary players;

        public RoomEngine(
            RoomId roomId,
            IChannelFactory channelFactory,
            ILogger logger)
        {
            this.roomId = roomId;
            this.logger = logger;

            roomChannel = channelFactory.CreateRoomChannel();

            gameLoopCancellationTokenSource = new CancellationTokenSource();
            processingCancellationTokenSource = new CancellationTokenSource();
            serviceMessages = new PooledList<IRoomServiceMessage>();
            players = new RoomPlayerDictionary();
        }

        public void StartGame()
        {
            Task.Factory.StartNew(() => RunGameLoop(gameLoopCancellationTokenSource.Token), TaskCreationOptions.LongRunning);

            var cancellationToken = processingCancellationTokenSource.Token;

            roomChannel.StartProcessing(message => MessageProcessor(message), cancellationToken);

            logger.LogInformation("Game started in room");
        }

        public ValueTask SendMessage(IRoomMessage message) => roomChannel.WriteAsync(message, processingCancellationTokenSource.Token);

        private async Task RunGameLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000, cancellationToken);
                    await roomChannel.WriteAsync(StepMessage.Instance, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private void MessageProcessor(IRoomMessage message)
        {
            try
            {
                ProcessMessage(message);
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Unhandled exception during game simulation in room");

                DebugExit();
            }
        }

        private void ProcessMessage(IRoomMessage message)
        {
            switch (message)
            {
                case StepMessage _:
                    Step();
                    break;
                case IRoomServiceMessage m:
                    serviceMessages.Add(m);
                    break;

                default:
                    throw new NotImplementedException($"The message of type {message.GetType()} is not supported");
            }
        }

        private void Step()
        {
            Console.WriteLine("Step");
            Update();

            /*while (!gameLoopCancellationTokenSource.IsCancellationRequested)
            {
                Update();
            }*/
        }

        private void Update()
        {
            ProcessServiceMessages();
            BroadcastSimulationState();
        }

        private void ProcessServiceMessages()
        {
            try
            {
                foreach (var serviceMessage in serviceMessages)
                {
                    ProcessRoomServiceMessage(serviceMessage);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                serviceMessages.Clear();
            }
        }

        private void ProcessRoomServiceMessage(IRoomServiceMessage message)
        {
            switch(message)
            {
                case JoinRoomMessage m:
                    if (players.TryGetValue(m.Client.ClientId, out RoomPlayer player))
                    {
                        //Reconnect
                    }
                    else
                    {
                        player = AddPlayer(m.Client);
                    }

                    player.SendMessage(new GameJoinedMessage(m.Client.ClientId));

                    break;
                default:
                    throw new NotImplementedException($"The message of type {message.GetType()} is not supported");
            }
        }

        private RoomPlayer AddPlayer(IClient client)
        {
            var player = new RoomPlayer(client);
            players.Add(player);

            return player;
        }

        private void BroadcastSimulationState()
        {

        }

        [Conditional("DEBUG")]
        private void DebugExit()
        {
            Thread.Sleep(2000);
            Environment.Exit(42);
        }
    }
}