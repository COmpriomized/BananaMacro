using System;
using System.Collections.Generic;

namespace BananaMacro.Models.Entities
{
    public class GearConfig
    {
        public string Id { get; set; } = IdGenerator.NewId();
        public string Name { get; set; } = "Default Gear";
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
        public DateTime CreatedAt { get; set; } = Timestamps.UtcNow;
        public DateTime? ModifiedAt { get; set; }
    }
}