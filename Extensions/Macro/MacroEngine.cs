using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BananaMacro.Extensions.Logging;
using BananaMacro.Models.Entities;

namespace BananaMacro.Extensions.Macro
{
    public class MacroEngine : IDisposable
    {
        private readonly ConcurrentDictionary<string, MacroRunner> _runners = new ConcurrentDictionary<string, MacroRunner>();
        private readonly LoggingService _log;

        public MacroEngine(LoggingService log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _log.Append(new LogEntry { Level = "Info", Source = "MacroEngine", Message = "MacroEngine initialized" });
        }

        public IEnumerable<string> GetActiveMacroIds() => _runners.Keys.ToArray();

        public bool IsRunning(MacroDefinition macro) => _runners.ContainsKey(macro.Id);

        public Task StartAsync(MacroDefinition macro)
        {
            if (macro == null) throw new ArgumentNullException(nameof(macro));
            if (_runners.ContainsKey(macro.Id)) return Task.CompletedTask;

            var runner = new MacroRunner(macro, _log);
            if (_runners.TryAdd(macro.Id, runner))
            {
                _ = runner.StartAsync().ContinueWith(t =>
                {
                    _runners.TryRemove(macro.Id, out _);
                });
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(MacroDefinition macro)
        {
            if (macro == null) throw new ArgumentNullException(nameof(macro));
            if (_runners.TryRemove(macro.Id, out var runner))
            {
                runner.Stop();
            }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            foreach (var kv in _runners.ToArray())
            {
                kv.Value.Stop();
            }
        }
    }
}