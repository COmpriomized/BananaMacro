using System;

namespace BananaMacro.Forums
{
    public class ForumPost
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsEdited { get; set; } = false;
    }
}