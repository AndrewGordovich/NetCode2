using System;
using NetCode2.Client.Network.Enet;
using NetCode2.Client.Realtime;
using NetCode2.Client.Realtime.Connection;
using UnityEngine;

namespace NetCode2.Client.Networking
{
    public class RealtimeRunBehavior : MonoBehaviour
    {
        private GameStateController stateController;

        private void Awake()
        {
            stateController = new GameStateController();
            stateController.StateChanged += StateControllerStateChanged;
        }

        private void Update() => stateController.UpdateCurrentState();

        public void Connect()
        {
            IGamePlayConnection connection = new EnetClient(new ENetClientSettings
            {
                ServerHostName = "127.0.0.1",
                //ServerHostName = "18.195.68.198",
                ServerPort = 40002
            });

            var entryState = new GameEntryState(stateController, connection);
            stateController.SetCurrentState(entryState);
        }

        private void StateControllerStateChanged(IGameState state)
        {
            switch(state)
            {
                case GameDisconnectedState disconnectedState:
                    Debug.Log($"State changed to {nameof(disconnectedState)}. Clean up resources");
                    break;
                case GameEntryState entryState:
                    Debug.Log($"State changed to {nameof(entryState)}.");
                    break;
                case GameRunningState runningState:
                    Debug.Log($"State changed to {nameof(runningState)}.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}