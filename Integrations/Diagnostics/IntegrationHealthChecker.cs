using System;
using System.Collections.Generic;
using System.Linq;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Diagnostics
{
    public static class IntegrationHealthChecker
    {
        public static IntegrationHealthReport Check(string id, IntegrationStatus status)
        {
            var issues = new List<string>();
            if (!status.IsHealthy) issues.Add("Unhealthy");
            if (status.LastHeartbeat == DateTime.MinValue) issues.Add("No heartbeat");

            return new IntegrationHealthReport
            {
                IntegrationId = id,
                Healthy = !issues.Any(),
                Issues = issues
            };
        }
    }
}