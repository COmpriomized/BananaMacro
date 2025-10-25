using System;

namespace BananaMacro.Integrations.Models
{
    public class IntegrationEvent
    {
        public string Source { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}