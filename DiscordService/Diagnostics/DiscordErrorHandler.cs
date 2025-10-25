using System;
using BananaMacro.Extensions.Logging;
using BananaMacro.Models.Entities;

namespace BananaMacro.DiscordService.Diagnostics
{
    public static class DiscordErrorHandler
    {
        public static void Handle(Exception ex, string context = "Discord")
        {
            var log = ServiceRegistry.Get<LoggingService>();
            log?.Append(new LogEntry
            {
                Level = "Error",
                Source = context,
                Message = ex.Message
            });

            Console.WriteLine($"[DiscordError] {context}: {ex.Message}");
        }
    }
}