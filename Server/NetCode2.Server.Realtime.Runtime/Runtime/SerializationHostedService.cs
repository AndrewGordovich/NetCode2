using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.Runtime.Runtime
{
    public class SerializationHostedService : IHostedService
    {
        private readonly ISerializationService serializationService;
        private readonly ILogger<SerializationHostedService> logger;

        public SerializationHostedService(ISerializationService serializationService, ILogger<SerializationHostedService> logger)
        {
            this.serializationService = serializationService;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await serializationService.StartAsync(cancellationToken);
            logger.LogInformation("{service} started.", nameof(SerializationHostedService));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await serializationService.StopAsync(cancellationToken);
            logger.LogInformation("{service} stopped.", nameof(SerializationHostedService));
        }
    }
}