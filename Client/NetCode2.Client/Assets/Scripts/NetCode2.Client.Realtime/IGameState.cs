using System;

namespace NetCode2.Client.Realtime
{
    public interface IGameState : IDisposable
    {
        void Update();
    }
}