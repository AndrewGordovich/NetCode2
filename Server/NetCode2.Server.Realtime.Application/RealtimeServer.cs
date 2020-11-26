using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Serialization;
using NetCode2.Server.Realtime.Contracts;
using NetCode2.Server.Realtime.Contracts.Channels;
using NetCode2.Server.Realtime.Contracts.Storages;
using NetCode2.Server.Realtime.Network.ENet;
using NetCode2.Server.Realtime.Runtime;
using NetCode2.Server.Realtime.Runtime.Channels;
using NetCode2.Server.Realtime.Runtime.Runtime;
using NetCode2.Server.Realtime.Runtime.Storages;
using NetCode2.Server.Realtime.Runtime.Storages.Contracts;

namespace NetCode2.Server.Realtime.Application
{
    public sealed class RealtimeServer
    {
        public static IHostBuilder Create(string basePath, string[] args)
        {
            return new HostBuilder()
                .UseEnvironment(Environments.Development)
                .UseContentRoot(basePath)
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    var env = hostBuilderContext.HostingEnvironment;

                    configurationBuilder
                        .SetBasePath(env.ContentRootPath)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddSingleton<IDeserializationChannel<ENetNetworkMessage>, DeserializationChannel<ENetNetworkMessage>>();
                    services.AddSingleton<IDeserializationService, DeserializationService<ENetNetworkMessage>>();

                    services.AddSingleton<INetworkServer, ENetServer>();
                    services.AddHostedService<ENetServerHostedService>();

                    services.AddSingleton<INetworkServerChannel, NetworkServerChannel>();

                    services.AddSingleton<IPeerStorage, PeerInMemoryStorage>();

                    services.AddHostedService<SerializationHostedService>();
                    services.AddHostedService<DeserializationHostedService>();

                    services.AddSingleton<IChannelFactory, ChannelFactory>();
                    services.AddSingleton<IChannelHandler, ChannelHandler>();

                    services.AddTransient<IRoomChannel, RoomChannel>();

                    services.AddSingleton<IClientManager, ClientManager>();
                    services.AddSingleton<IClientFactory, ClientFactory>();
                    services.AddSingleton<IClientStorage, ClientInMemoryStorage>();

                    services.AddSingleton<ISerializationChannel, SerializationChannel>();
                    services.AddSingleton<ISerializationService, SerializationService>();

                    services.AddTransient<GameJoinedEventDataSerializer>();

                    services.AddTransient<SimulationCommandsSerializer>();

                    services.AddSingleton<IRoomStorage, RoomInMemoryStorage>();

                    services.AddTransient<IDataSerializer<JoinGameCommandData>, JoinGameCommandDataSerializer>();
                });
        }
    }
}