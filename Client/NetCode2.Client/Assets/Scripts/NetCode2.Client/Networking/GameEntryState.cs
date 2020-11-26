using System;
using System.Collections.Generic;
using NetCode2.Client.Realtime;
using NetCode2.Client.Realtime.Connection;
using NetCode2.Client.Realtime.Service;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Data.Events;
using NetCode2.Common.Realtime.Service;
using UniRx;
using UnityEngine;

namespace NetCode2.Client.Networking
{
    public class GameEntryState : IGameState
    {
        private readonly GameStateController stateController;
        private readonly IGamePlayConnection connection;
        private readonly INetworkService networkService;

        private readonly CompositeDisposable subscriptions = new CompositeDisposable();

        private State state = State.ConnectingToServer;

        public GameEntryState(
            GameStateController stateController,
            IGamePlayConnection connection)
        {
            this.stateController = stateController;
            this.connection = connection;

            var gameJoinedDataHandler = new GameJoinedDataHandler();

            var handlers = new Dictionary<NetworkDataCode, IDataHandler>
            {
                {NetworkDataCode.GameJoined, gameJoinedDataHandler}
            };

            networkService = new NetworkService(connection, handlers);

            Observable.FromEvent<GameJoinedEventData>(
                    h => gameJoinedDataHandler.GameJoined += h,
                    h => gameJoinedDataHandler.GameJoined -= h)
                .Subscribe(GameJoinedHandler)
                .AddTo(subscriptions);
        }

        public void Update()
        {
            networkService.Receive();

            switch(state)
            {
                case State.ConnectingToServer:
                    ConnectToServerAndJoinRoom();
                    break;
                case State.JoiningGame:
                    break;
                case State.Ready:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            networkService.Send();
        }

        public void Dispose()
        {
            subscriptions.Dispose();
        }

        private void ConnectToServerAndJoinRoom()
        {
            switch(connection.ConnectionState)
            {
                case ConnectionState.Disconnected:
                    Debug.Log($"Trying to connect to server");
                    connection.Connect();
                    break;
                case ConnectionState.Connecting:
                    Debug.Log($"Connecting to realtime server...");
                    break;
                case ConnectionState.Connected:
                    Debug.LogError($"Connected to realtime server!");
                    networkService.QueueAndSendCommand(new JoinGameCommandData());
                    state = State.JoiningGame;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameJoinedHandler(GameJoinedEventData gameJoinedData)
        {
            var room = new Room(connection);

            var nextState = new GameRunningState(stateController, room);
            stateController.SetCurrentState(nextState);
            state = State.Ready;

            room.StartGame();
        }

        private enum State
        {
            ConnectingToServer,
            JoiningGame,
            Ready,
        }
    }
}