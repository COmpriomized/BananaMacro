using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.DiscordService.Core;
using BananaMacro.DiscordService.Interfaces;
using BananaMacro.Integrations.Interfaces;
using BananaMacro.Integrations.Models;
using BananaMacro.Extensions.Logging;
using BananaMacro.Extensions.Core;

namespace BananaMacro.Integrations.Services
{
    public class DiscordIntegration : IIntegration
    {
        public string Id { get; } = "discord";
        public string Name { get; } = "Discord Integration";
        public IntegrationStatus Status { get; private set; } = new IntegrationStatus();
        public event Func<IntegrationEvent, Task>? OnEvent;

        private readonly IDiscordClient _client;
        private readonly LoggingService _log;
        private IntegrationConfig? _config;

        public DiscordIntegration()
        {
            _client = ServiceRegistry.Get<IDiscordClient>() ?? new DiscordClient();
            _log = ServiceRegistry.Get<LoggingService>() ?? new LoggingService();
        }

        public Task InitializeAsync(IntegrationConfig config, CancellationToken ct = default)
        {
            _config = config;
            Status.State = "Initialized";
            _log.Append(new Models.Entities.LogEntry { Level = "Info", Source = "DiscordIntegration", Message = $"Initialized with id={config.Id}" });
            _client.OnCommandReceived += Client_OnCommandReceived;
            Status.IsHealthy = true;
            Status.LastHeartbeat = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        private async Task Client_OnCommandReceived(string raw)
        {
            var ie = new IntegrationEvent { Source = Id, Type = "CommandReceived", Payload = raw };
            if (OnEvent != null) await OnEvent.Invoke(ie);
        }

        public Task StartAsync(CancellationToken ct = default)
        {
            Status.State = "Starting";
            Status.State = "Running";
            Status.LastHeartbeat = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken ct = default)
        {
            Status.State = "Stopped";
            Status.LastHeartbeat = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _client.OnCommandReceived -= Client_OnCommandReceived;
        }
    }
}