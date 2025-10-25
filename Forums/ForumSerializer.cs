using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.Forums
{
    public static class ForumSerializer
    {
        public static async Task SaveAsync(IEnumerable<ForumThread> threads, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(threads, options);
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<List<ForumThread>> LoadAsync(string path)
        {
            if (!File.Exists(path)) return new List<ForumThread>();
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<ForumThread>>(json) ?? new List<ForumThread>();
        }
    }
}