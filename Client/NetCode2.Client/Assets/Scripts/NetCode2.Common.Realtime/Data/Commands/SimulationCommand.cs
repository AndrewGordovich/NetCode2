namespace NetCode2.Common.Realtime.Data.Commands
{
    public class SimulationCommand
    {
        public IGameCommand GameCommand { get; }

        public SimulationCommand(IGameCommand gameCommand)
        {
            GameCommand = gameCommand;
        }
    }
}