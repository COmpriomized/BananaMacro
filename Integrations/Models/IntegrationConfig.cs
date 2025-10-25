namespace BananaMacro.Integrations.Models
{
    public class IntegrationConfig
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
        public string? Endpoint { get; set; }
        public bool Enabled { get; set; } = true;
        public string? SettingsJson { get; set; }
    }
}