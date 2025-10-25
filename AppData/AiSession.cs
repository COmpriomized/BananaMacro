using System;
using System.Collections.Generic;

namespace BananaMacro.AppData
{
    public class AiSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public List<string> Inputs { get; set; } = new();
        public List<string> Outputs { get; set; } = new();
        public Dictionary<string, string> Metadata { get; set; } = new();
    }
}