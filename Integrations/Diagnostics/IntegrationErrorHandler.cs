using System;
using BananaMacro.Extensions.Logging;
using BananaMacro.Extensions.Core;

namespace BananaMacro.Integrations.Diagnostics
{
    public static class IntegrationErrorHandler
    {
        public static void Report(Exception ex, string context = "Integration")
        {
            var log = ServiceRegistry.Get<LoggingService>();
            log?.Append(new Models.Entities.LogEntry { Level = "Error", Source = context, Message = ex.Message });
            Console.WriteLine($"[IntegrationError] {context}: {ex.Message}");
        }
    }
}