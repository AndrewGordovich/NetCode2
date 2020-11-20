using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Messages;
using NetCode2.Server.Realtime.RoomEngine.Channels.RoomMessages;

namespace NetCode2.Server.Realtime.RoomEngine.Gameplay
{
    public sealed class RoomEngine
    {
        private readonly ILogger logger;
        private readonly IRoomChannel roomChannel;

        private readonly CancellationTokenSource gameLoopCancellationTokenSource;
        private readonly CancellationTokenSource processingCancellationTokenSource;

        private RoomPlayer roomPlayer;

        public RoomEngine(
            IChannelFactory channelFactory,
            ILogger logger)
        {
            this.logger = logger;

            roomChannel = channelFactory.CreateRoomChannel();

            gameLoopCancellationTokenSource = new CancellationTokenSource();
            processingCancellationTokenSource = new CancellationTokenSource();
        }

        public void StartGame()
        {
            Task.Factory.StartNew(() => RunGameLoop(gameLoopCancellationTokenSource.Token), TaskCreationOptions.LongRunning);

            var cancellationToken = processingCancellationTokenSource.Token;

            roomChannel.StartProcessing(message => MessageProcessor(message), cancellationToken);

            logger.LogInformation("Game started in room");
        }

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
            }
        }

        private void Step()
        {
            Console.WriteLine("Step");

            /*while (!gameLoopCancellationTokenSource.IsCancellationRequested)
            {
                Update();
            }*/
        }

        private void Update()
        {
            BroadcastSimulationState();
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