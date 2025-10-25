using System.Collections.Generic;

namespace BananaMacro.Integrations.Models
{
    public class IntegrationHealthReport
    {
        public string IntegrationId { get; set; } = string.Empty;
        public bool Healthy { get; set; }
        public IEnumerable<string>? Issues { get; set; }
    }
}