using System.Text.Json;

namespace BananaMacro.Themes
{
    public static class ThemeSerializer
    {
        public static string Serialize(ThemeModel model)
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(model, opts);
        }

        public static ThemeModel? Deserialize(string json)
        {
            try { return JsonSerializer.Deserialize<ThemeModel>(json); }
            catch { return null; }
        }
    }
}