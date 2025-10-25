using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaMacro.Forums
{
    public class ForumService
    {
        private readonly List<ForumThread> _threads = new();

        public IEnumerable<ForumThread> GetAllThreads() => _threads;

        public ForumThread? GetThreadById(string id) => _threads.FirstOrDefault(t => t.Id == id);

        public ForumThread CreateThread(string title, string author, List<string>? tags = null)
        {
            var thread = new ForumThread
            {
                Title = title,
                Author = author,
                Tags = tags ?? new List<string>()
            };
            _threads.Add(thread);
            return thread;
        }

        public bool AddPost(string threadId, ForumPost post)
        {
            var thread = GetThreadById(threadId);
            if (thread == null || thread.IsLocked) return false;
            thread.Posts.Add(post);
            return true;
        }

        public bool LockThread(string threadId)
        {
            var thread = GetThreadById(threadId);
            if (thread == null) return false;
            thread.IsLocked = true;
            return true;
        }

        public IEnumerable<ForumThread> SearchByTag(string tag)
        {
            return _threads.Where(t => t.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase));
        }

        public IEnumerable<ForumThread> SearchByKeyword(string keyword)
        {
            return _threads.Where(t =>
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Posts.Any(p => p.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
        }
    }
}