using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Realtime.Runtime.Runtime
{
    public class DeserializationHostedService : IHostedService
    {
        private readonly IDeserializationService deserializationService;
        private readonly ILogger<DeserializationHostedService> logger;

        public DeserializationHostedService(IDeserializationService deserializationService, ILogger<DeserializationHostedService> logger)
        {
            this.deserializationService = deserializationService;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await deserializationService.StartAsync(cancellationToken);
            logger.LogInformation("{service} started.", nameof(DeserializationHostedService));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await deserializationService.StopAsync(cancellationToken);
            logger.LogInformation("{service} stopped.", nameof(DeserializationHostedService));
        }
    }
}