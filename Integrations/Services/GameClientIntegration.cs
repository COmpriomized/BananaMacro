using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Integrations.Interfaces;
using BananaMacro.Integrations.Models;
using BananaMacro.Extensions.Logging;
using BananaMacro.Extensions.Core;

namespace BananaMacro.Integrations.Services
{
    public class GameClientIntegration : IIntegration
    {
        public string Id { get; } = "gameclient";
        public string Name { get; } = "Game Client Integration";
        public IntegrationStatus Status { get; private set; } = new IntegrationStatus();
        public event Func<IntegrationEvent, Task>? OnEvent;

        private readonly LoggingService _log;
        private IntegrationConfig? _config;
        private Timer? _pollTimer;

        public GameClientIntegration(LoggingService? log = null)
        {
            _log = log ?? ServiceRegistry.Get<LoggingService>()!;
        }

        public Task InitializeAsync(IntegrationConfig config, CancellationToken ct = default)
        {
            _config = config;
            Status.State = "Initialized";
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken ct = default)
        {
            Status.State = "Running";
            _pollTimer = new Timer(async _ => await PollAsync(), null, 0, 1000);
            return Task.CompletedTask;
        }

        private Task PollAsync()
        {
            var e = new IntegrationEvent { Source = Id, Type = "Heartbeat", Payload = "OK" };
            return OnEvent != null ? OnEvent.Invoke(e) : Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken ct = default)
        {
            _pollTimer?.Dispose();
            Status.State = "Stopped";
            return Task.CompletedTask;
        }

        public void Dispose() => _pollTimer?.Dispose();
    }
}