using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailService;
using LoggingService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailLogger _emailLogger;
        private FileSystemWatcher watcher;
        private readonly string path = @"/Users/alisher/Documents/worker";

        public Worker(ILogger<Worker> logger, IEmailLogger emailLogger)
        {
            _logger = logger;
            _emailLogger = emailLogger;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;

            watcher.Created += OnCreated;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;

            return base.StartAsync(cancellationToken);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _emailLogger.OnChanged(e);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _emailLogger.OnDeleted(e);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _emailLogger.OnRenamed(e);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _emailLogger.OnCreated(e);
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                watcher.EnableRaisingEvents = true;
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }


    }
}
