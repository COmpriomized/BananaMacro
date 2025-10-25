using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Integrations.Interfaces;
using BananaMacro.Integrations.Models;
using BananaMacro.Extensions.Interfaces;
using BananaMacro.Extensions.Logging;

namespace BananaMacro.Integrations.Services
{
    public class CloudSyncIntegration : IIntegration
    {
        public string Id { get; } = "cloudsync";
        public string Name { get; } = "Cloud Sync";
        public IntegrationStatus Status { get; private set; } = new IntegrationStatus();
        public event Func<IntegrationEvent, Task>? OnEvent;

        private readonly IStore _store;
        private readonly LoggingService _log;
        private IntegrationConfig? _config;

        public CloudSyncIntegration(IStore? store = null, LoggingService? log = null)
        {
            _store = store ?? ServiceRegistry.Get<IStore>()!;
            _log = log ?? ServiceRegistry.Get<LoggingService>()!;
        }

        public async Task InitializeAsync(IntegrationConfig config, CancellationToken ct = default)
        {
            _config = config;
            Status.State = "Initialized";
            _log.Append(new Models.Entities.LogEntry { Level = "Info", Source = "CloudSync", Message = $"Initialized {config.Id}" });
            await Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken ct = default)
        {
            Status.State = "Running";
            Status.IsHealthy = true;
            Status.LastHeartbeat = DateTime.UtcNow;

            try
            {
                var files = await _store.ListFilesAsync("saved").ConfigureAwait(false);
                await Task.Delay(10, ct);
                if (OnEvent != null) await OnEvent.Invoke(new IntegrationEvent { Source = Id, Type = "Synced", Payload = string.Join(",", files) });
            }
            catch (Exception ex)
            {
                Status.IsHealthy = false;
                Status.LastError = ex.Message;
            }
        }

        public Task StopAsync(CancellationToken ct = default)
        {
            Status.State = "Stopped";
            return Task.CompletedTask;
        }

        public void Dispose() { }
    }
}