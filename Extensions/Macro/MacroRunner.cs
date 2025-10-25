using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Models.Entities;
using BananaMacro.Extensions.Logging;

namespace BananaMacro.Extensions.Macro
{
    public class MacroRunner
    {
        public MacroDefinition Macro { get; }
        public CancellationTokenSource Cts { get; private set; }
        public bool IsRunning => Cts != null && !Cts.IsCancellationRequested;

        private readonly LoggingService _log;

        public MacroRunner(MacroDefinition macro, LoggingService log)
        {
            Macro = macro ?? throw new ArgumentNullException(nameof(macro));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public Task StartAsync()
        {
            if (IsRunning) return Task.CompletedTask;
            Cts = new CancellationTokenSource();
            return Task.Run(() => RunLoopAsync(Cts.Token));
        }

        public void Stop()
        {
            if (!IsRunning) return;
            Cts.Cancel();
            _log.Append(new LogEntry { Level = "Info", Source = "MacroRunner", Message = $"Stopping macro {Macro.Name}" });
        }

        private async Task RunLoopAsync(CancellationToken token)
        {
            _log.Append(new LogEntry { Level = "Info", Source = "MacroRunner", Message = $"Starting macro {Macro.Name}" });
            try
            {
                // Placeholder execution: pretend to execute script lines with delay
                var script = Macro.Script ?? string.Empty;
                var lines = script.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    token.ThrowIfCancellationRequested();
                    _log.Append(new LogEntry { Level = "Debug", Source = "MacroRunner", Message = $"Executing: {line.Trim()}" });
                    await Task.Delay(250, token).ConfigureAwait(false);
                }

                // keep running if macro is long-lived (example idle loop)
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                _log.Append(new LogEntry { Level = "Info", Source = "MacroRunner", Message = $"Macro {Macro.Name} cancelled" });
            }
            catch (Exception ex)
            {
                _log.Append(new LogEntry { Level = "Error", Source = "MacroRunner", Message = $"Macro {Macro.Name} failed: {ex.Message}" });
            }
            finally
            {
                _log.Append(new LogEntry { Level = "Info", Source = "MacroRunner", Message = $"Macro {Macro.Name} stopped" });
                Cts = null;
            }
        }
    }
}