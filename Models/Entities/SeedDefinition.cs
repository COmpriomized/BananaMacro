using System;

namespace BananaMacro.Models.Entities
{
    public class SeedDefinition
    {
        public string Id { get; set; } = IdGenerator.NewId();
        public string Name { get; set; } = "New Seed";
        public string Value { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = Timestamps.UtcNow;
    }
}