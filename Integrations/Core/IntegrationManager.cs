using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Extensions.Core;
using BananaMacro.Extensions.Interfaces;
using BananaMacro.Extensions.Logging;
using BananaMacro.Integrations.Interfaces;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Core
{
    public class IntegrationManager : IDisposable
    {
        private readonly ConcurrentDictionary<string, IIntegration> _integrations = new();
        private readonly IntegrationContext _context;

        public IntegrationManager(IntegrationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<string> Registered => _integrations.Keys.ToArray();

        public void Register(string id, IIntegration integration)
        {
            if (integration == null) throw new ArgumentNullException(nameof(integration));
            _integrations[id] = integration;
            integration.OnEvent += HandleIntegrationEvent;
        }

        public bool TryGet(string id, out IIntegration? integration) => _integrations.TryGetValue(id, out integration);

        public async Task InitializeAllAsync(IEnumerable<IntegrationConfig> configs, CancellationToken ct = default)
        {
            foreach (var cfg in configs)
            {
                if (!cfg.Enabled) continue;
                var integration = CreateOrGet(cfg.Id);
                try
                {
                    await integration.InitializeAsync(cfg, ct).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _context.Logger?.Append(new Models.Entities.LogEntry { Level = "Error", Source = "IntegrationManager", Message = $"Init failed for {cfg.Id}: {ex.Message}" });
                }
            }
        }

        public async Task StartAllAsync(CancellationToken ct = default)
        {
            var tasks = _integrations.Values.Select(i => SafeStart(i, ct));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private static async Task SafeStart(IIntegration integration, CancellationToken ct)
        {
            try { await integration.StartAsync(ct).ConfigureAwait(false); } catch { }
        }

        public async Task StopAllAsync()
        {
            foreach (var kv in _integrations)
            {
                try { await kv.Value.StopAsync().ConfigureAwait(false); } catch { }
            }
        }

        public void Unregister(string id)
        {
            if (_integrations.TryRemove(id, out var integration))
            {
                integration.OnEvent -= HandleIntegrationEvent;
                integration.Dispose();
            }
        }

        private Task HandleIntegrationEvent(IntegrationEvent e)
        {
            _context.Logger.Append(new Models.Entities.LogEntry { Level = "Info", Source = $"Integration:{e.Source}", Message = $"{e.Type} {e.Payload}" });
            return Task.CompletedTask;
        }

        private IIntegration CreateOrGet(string id)
        {
            if (!_integrations.TryGetValue(id, out var integration))
            {
                var provider = ServiceRegistry.Get<IIntegrationProvider>();
                integration = provider?.Create(id) ?? throw new InvalidOperationException($"No provider for {id}");
                Register(id, integration);
            }
            return integration;
        }

        public void Dispose()
        {
            foreach (var kv in _integrations) kv.Value.Dispose();
            _integrations.Clear();
        }
    }
}