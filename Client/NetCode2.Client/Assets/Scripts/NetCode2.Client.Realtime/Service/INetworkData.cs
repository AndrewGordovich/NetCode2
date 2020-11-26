using System;
using NetCode2.Common.Realtime.Service;

namespace NetCode2.Client.Realtime.Service
{
    public interface INetworkData
    {
        Span<byte> Span { get; }

        NetworkDataCode Code { get; }

        int Length { get; }

        void Dispose();
    }
}