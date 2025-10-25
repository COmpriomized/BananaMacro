namespace BananaMacro.Models.Entities
{
    public class PluginManifest
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = "1.0.0";
        public string Assembly { get; set; } = string.Empty;
        public string EntryType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[]? Permissions { get; set; }
        public string? MinimumHostVersion { get; set; }
    }
}