namespace BananaMacro.Models.Entities
{
    public class AppSettings
    {
        public string LastOpenedMacroId { get; set; } = string.Empty;
        public string Theme { get; set; } = "LightBlack";
        public bool EnableTelemetry { get; set; } = false;
        public string PreferredFont { get; set; } = "Space Mono";
    }
}