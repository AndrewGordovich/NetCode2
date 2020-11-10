using System;
using System.Threading;

namespace NetCode2.Server.Realtime.Contracts.Channels
{
    public interface IMessageChannel<TMessage>
    {
        void StartProcessing(Action<TMessage> processor, CancellationToken cancellationToken = default);

        void StipProcessing();
    }
}