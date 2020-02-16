using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace worker_service.Worker
{
    public class DefaultWorker : BackgroundService
    {
        private readonly ILogger<DefaultWorker> _logger;
        public static string _TaskResult = String.Empty;
        public DefaultWorker(ILogger<DefaultWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _TaskResult = string.Format("Worker running at: {0}", DateTimeOffset.Now);
                _logger.LogInformation(_TaskResult);
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}