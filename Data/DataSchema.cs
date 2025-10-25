namespace BananaMacro.Data
{
    public class MacroProfile
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Tags { get; set; } = Array.Empty<string>();
        public string Script { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }

    public class UserStats
    {
        public int MacrosCreated { get; set; }
        public int MacrosRun { get; set; }
        public TimeSpan TotalRuntime { get; set; }
        public DateTime LastActive { get; set; }
    }
}