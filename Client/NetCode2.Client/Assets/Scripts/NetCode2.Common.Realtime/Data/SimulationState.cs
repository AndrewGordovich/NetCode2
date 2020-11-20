using System;

namespace NetCode2.Common.Realtime.Data
{
    public struct SimulationState : IDisposable
    {
        public readonly int Tick;

        public GameState GameState;

        public SimulationState(int tick, GameState gameState)
        {
            Tick = tick;
            GameState = gameState;
        }

        public void Dispose()
        {
            GameState.Dispose();
        }
    }
}