using System;

namespace NetCode2.Client.Realtime
{
    public class GameStateController
    {
        private IGameState currentState;

        public GameStateController() => currentState = new GameDisconnectedState(DisconnectReason.GameEnded);

        public event Action<IGameState> StateChanged;

        public void SetCurrentState(IGameState state)
        {
            currentState.Dispose();
            currentState = state.NotNull();
            StateChanged?.Invoke(state);
        }

        public void UpdateCurrentState() => currentState.Update();
    }
}