using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.GearConfig
{
    public static class GearLoader
    {
        public static async Task<List<GearModel>> LoadAsync(string path)
        {
            if (!File.Exists(path)) return new List<GearModel>();
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<GearModel>>(json) ?? new List<GearModel>();
        }
    }
}