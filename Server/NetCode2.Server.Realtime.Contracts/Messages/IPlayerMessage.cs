namespace NetCode2.Server.Realtime.Contracts.Messages
{
    public interface IPlayerMessage : IMessage
    {
        ClientId ClientId { get; }
    }
}