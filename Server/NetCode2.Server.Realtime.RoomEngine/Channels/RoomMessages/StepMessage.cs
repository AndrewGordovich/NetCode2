using NetCode2.Server.Realtime.Contracts.Messages;

namespace NetCode2.Server.Realtime.RoomEngine.Channels.RoomMessages
{
    internal sealed class StepMessage : IRoomMessage
    {
        static StepMessage() => Instance = new StepMessage();

        private StepMessage()
        {
        }

        public static StepMessage Instance { get; }
    }
}