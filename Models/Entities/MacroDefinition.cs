using System;

namespace BananaMacro.Models.Entities
{
    public class MacroDefinition
    {
        public string Id { get; set; } = IdGenerator.NewId();
        public string Name { get; set; } = "New Macro";
        public string Script { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public DateTime CreatedAt { get; set; } = Timestamps.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}