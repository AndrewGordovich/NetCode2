namespace NetCode2.Client.Realtime
{
    public class GameRunningState : IGameState
    {
        private readonly GameStateController stateController;

        public Room Room { get; }

        public GameRunningState(GameStateController stateController, Room room)
        {
            Room = room;
            this.stateController = stateController;
        }

        public void Update()
        {
            Room.Update();
        }

        public void Dispose()
        {
        }
    }
}