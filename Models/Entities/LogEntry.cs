using System;

namespace BananaMacro.Models.Entities
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = Timestamps.UtcNow;
        public string Level { get; set; } = "Info";
        public string Source { get; set; } = "Core";
        public string Message { get; set; } = string.Empty;
        public override string ToString() => $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Level,-5} {Source} - {Message}";
    }
}