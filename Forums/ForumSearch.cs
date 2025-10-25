using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaMacro.Forums
{
    public static class ForumSearch
    {
        public static IEnumerable<ForumThread> ByKeyword(IEnumerable<ForumThread> threads, string keyword)
        {
            return threads.Where(t =>
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Posts.Any(p => p.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
        }

        public static IEnumerable<ForumThread> ByTag(IEnumerable<ForumThread> threads, string tag)
        {
            return threads.Where(t => t.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase));
        }

        public static IEnumerable<ForumThread> ByAuthor(IEnumerable<ForumThread> threads, string author)
        {
            return threads.Where(t =>
                t.Author.Equals(author, StringComparison.OrdinalIgnoreCase) ||
                t.Posts.Any(p => p.Author.Equals(author, StringComparison.OrdinalIgnoreCase)));
        }

        public static IEnumerable<ForumPost> PostsByKeyword(IEnumerable<ForumThread> threads, string keyword)
        {
            return threads.SelectMany(t => t.Posts)
                          .Where(p => p.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}