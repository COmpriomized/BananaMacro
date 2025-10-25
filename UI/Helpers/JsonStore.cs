using System.IO;
using System.Text.Json;

namespace BananaMacro.UI.UIHelpers
{
    public static class JsonStore
    {
        public static T Load<T>(string path) where T : new()
        {
            if (!File.Exists(path)) return new T();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static void Save<T>(string path, T data)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}