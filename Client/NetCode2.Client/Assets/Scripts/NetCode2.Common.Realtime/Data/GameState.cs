using System;

namespace NetCode2.Common.Realtime.Data
{
    public struct GameState : IDisposable
    {
        public byte Position;

        public GameState(byte position)
        {
            Position = position;
        }

        public void Dispose()
        {

        }
    }
}