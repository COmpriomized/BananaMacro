using System;
using BananaMacro.Extensions.Core;
using BananaMacro.Extensions.Logging;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Diagnostics
{
    /// <summary>
    /// Forwards application log entries into integration events so integrations can react or expose them.
    /// Also implements ILogSink for optional direct wiring into logging pipelines.
    /// </summary>
    public class IntegrationLogger : ILogSink, IDisposable
    {
        private readonly Action<IntegrationEvent>? _onIntegrationEvent;

        public IntegrationLogger(Action<IntegrationEvent>? onIntegrationEvent = null)
        {
            _onIntegrationEvent = onIntegrationEvent;
        }

        /// <summary>
        /// Emit called by logging pipeline. Converts LogEntry into IntegrationEvent and forwards it.
        /// Avoid throwing to keep logging non-fatal.
        /// </summary>
        public void Emit(Models.Entities.LogEntry entry)
        {
            try
            {
                if (entry == null) return;

                var ie = new IntegrationEvent
                {
                    Source = "ApplicationLog",
                    Type = entry.Level ?? "Info",
                    Payload = entry.Message ?? string.Empty,
                    Timestamp = entry.Timestamp
                };

                // If an IntegrationManager or other subscriber exists in ServiceRegistry, dispatch to it
                var manager = ServiceRegistry.Get<Core.IntegrationManager>();
                if (manager != null)
                {
                    // We fire-and-forget the handler so logging doesn't block
                    _ = manager.GetType()
                               .GetMethod("HandleIntegrationEvent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                               ?.Invoke(manager, new object[] { ie }) as System.Threading.Tasks.Task;
                }

                // Also notify the provided callback if present
                _onIntegrationEvent?.Invoke(ie);
            }
            catch
            {
                // Swallow exceptions to ensure logging never causes application failure
            }
        }

        public void Dispose()
        {
            // Nothing to dispose by default; present for DI/cleanup
        }
    }
}