namespace NetCode2.Server.Realtime.Contracts.Messages
{
    public readonly struct NetworkMessage
    {
        public NetworkMessage(PeerId peerId, byte[] data, int length, bool isReliable)
        {
            PeerId = peerId;
            Data = data;
            Length = length;
            IsReliable = isReliable;
        }

        public PeerId PeerId { get; }

        public byte[] Data { get; }

        public int Length { get; }

        public bool IsReliable { get; }
    }
}