using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.AppData
{
    public static class AiDataLoader
    {
        public static async Task SaveAsync<T>(T data, string path)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<T?> LoadAsync<T>(string path)
        {
            if (!File.Exists(path)) return default;
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}