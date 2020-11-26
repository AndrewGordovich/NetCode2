namespace NetCode2.Client.Realtime
{
    public class GameDisconnectedState : IGameState
    {
        public DisconnectReason DisconnectReason { get; private set; }

        public GameDisconnectedState(DisconnectReason disconnectReason)
        {
            DisconnectReason = disconnectReason;
        }

        public void Update()
        {
        }

        public void Dispose()
        {
            
        }
    }
}