using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Extensions.Logging;
using BananaMacro.Models.Entities;

namespace BananaMacro.Tests.Utilities
{
    public class TestLoggingSink : IDisposable
    {
        private readonly ConcurrentQueue<LogEntry> _entries = new ConcurrentQueue<LogEntry>();
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();
        private readonly LoggingService? _loggingService;

        public TestLoggingSink(LoggingService? loggingService = null)
        {
            _loggingService = loggingService;
            if (_loggingService != null)
            {
                // subscribe to the LoggingService event
                _loggingService.OnLogAppended += OnLogAppended;
            }
        }

        private void OnLogAppended(LogEntry entry)
        {
            if (entry == null) return;
            _entries.Enqueue(entry);
        }

        public IReadOnlyList<LogEntry> Entries => _entries.ToArray();

        public int Count => _entries.Count;

        public void Clear()
        {
            while (_entries.TryDequeue(out _)) { }
        }

        public bool ContainsMessage(string containsText)
        {
            return _entries.Any(e => e?.Message?.IndexOf(containsText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IEnumerable<LogEntry> PopAll()
        {
            var list = new List<LogEntry>();
            while (_entries.TryDequeue(out var e)) list.Add(e);
            return list;
        }

        public async Task<LogEntry?> WaitForEntryAsync(Func<LogEntry, bool> predicate, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
        {
            var deadline = DateTime.UtcNow + (timeout ?? TimeSpan.FromSeconds(5));
            while (!cancellationToken.IsCancellationRequested && DateTime.UtcNow <= deadline)
            {
                foreach (var e in _entries.ToArray())
                {
                    if (predicate(e)) return e;
                }

                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            return null;
        }

        public void Dispose()
        {
            if (_loggingService != null)
            {
                _loggingService.OnLogAppended -= OnLogAppended;
            }
        }
    }
}