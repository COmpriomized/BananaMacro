using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Models.Entities;
using BananaMacro.Extensions.Interfaces;

namespace BananaMacro.Extensions.Logging
{
    public class LoggingService : IDisposable
    {
        private readonly BlockingCollection<LogEntry> _queue = new BlockingCollection<LogEntry>(new ConcurrentQueue<LogEntry>());
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Task _worker;
        private readonly IStore _store;
        private readonly string _logFolder;
        private readonly string _currentLogPath;

        public event Action<LogEntry> OnLogAppended;

        public LoggingService(IStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(_logFolder);
            _currentLogPath = Path.Combine(_logFolder, $"bananamacro-{DateTime.UtcNow:yyyyMMddHHmmss}.log");
            _worker = Task.Run(ProcessQueueAsync);
            Append(new LogEntry { Level = "Info", Source = "LoggingService", Message = "Logging started (theme: LightBlack)" });
        }

        public void Append(LogEntry entry)
        {
            if (entry == null) return;
            entry.Timestamp = entry.Timestamp == default ? DateTime.UtcNow : entry.Timestamp;
            _queue.Add(entry);
            OnLogAppended?.Invoke(entry);
        }

        private async Task ProcessQueueAsync()
        {
            try
            {
                using var sw = new StreamWriter(_currentLogPath, append: true);
                while (!_cts.IsCancellationRequested)
                {
                    LogEntry entry;
                    try
                    {
                        entry = _queue.Take(_cts.Token);
                    }
                    catch (OperationCanceledException) { break; }

                    var line = entry.ToString();
                    await sw.WriteLineAsync(line).ConfigureAwait(false);
                    await sw.FlushAsync().ConfigureAwait(false);
                }

                // flush remaining
                while (_queue.TryTake(out var remaining))
                {
                    await File.AppendAllTextAsync(_currentLogPath, remaining.ToString() + Environment.NewLine).ConfigureAwait(false);
                }
            }
            catch { /* swallow exceptions to keep logging resilient */ }
        }

        public void RotateLog()
        {
            Append(new LogEntry { Level = "Info", Source = "LoggingService", Message = "Rotating log file" });
            // rotation is handled by naming; a new instance would create a new file
        }

        public void Dispose()
        {
            _cts.Cancel();
            _worker.Wait(2000);
            _queue.Dispose();
            _cts.Dispose();
        }
    }
}