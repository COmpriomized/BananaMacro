using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.SeedConfig
{
    public static class SeedLoader
    {
        public static async Task<List<SeedModel>> LoadAsync(string path)
        {
            if (!File.Exists(path)) return new List<SeedModel>();
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<SeedModel>>(json) ?? new List<SeedModel>();
        }
    }
}