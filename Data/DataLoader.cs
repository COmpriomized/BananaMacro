using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.Data
{
    public static class DataLoader
    {
        public static async Task SaveAsync<T>(IEnumerable<T> data, string path)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<List<T>> LoadAsync<T>(string path)
        {
            if (!File.Exists(path)) return new List<T>();
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}