using System;
using Microsoft.Extensions.DependencyInjection;
using NetCode2.Server.Realtime.Contracts.Channels;

namespace NetCode2.Server.Realtime.Runtime.Channels
{
    public class ChannelFactory : IChannelFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ChannelFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IRoomChannel CreateRoomChannel() => serviceProvider.GetRequiredService<IRoomChannel>();
    }
}