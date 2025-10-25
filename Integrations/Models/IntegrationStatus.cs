using System;

namespace BananaMacro.Integrations.Models
{
    public class IntegrationStatus
    {
        public bool IsHealthy { get; set; } = false;
        public DateTime LastHeartbeat { get; set; } = DateTime.MinValue;
        public string? LastError { get; set; }
        public string State { get; set; } = "Stopped";
    }
}