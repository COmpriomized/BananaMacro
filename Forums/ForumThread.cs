using System;
using System.Collections.Generic;

namespace BananaMacro.Forums
{
    public class ForumThread
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<ForumPost> Posts { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsLocked { get; set; } = false;
    }
}