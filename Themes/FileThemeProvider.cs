using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.Themes
{
    public class FileThemeProvider : IThemeProvider
    {
        private readonly string _themesFolder;

        public FileThemeProvider(string themesFolder)
        {
            _themesFolder = themesFolder ?? throw new ArgumentNullException(nameof(themesFolder));
            Directory.CreateDirectory(_themesFolder);
        }

        public async Task<IEnumerable<ThemeModel>> GetAvailableAsync()
        {
            var files = Directory.GetFiles(_themesFolder, "*.theme.json");
            var list = new List<ThemeModel>();
            foreach (var f in files)
            {
                try
                {
                    var text = await File.ReadAllTextAsync(f).ConfigureAwait(false);
                    var model = JsonSerializer.Deserialize<ThemeModel>(text);
                    if (model != null) list.Add(model);
                }
                catch
                {
                    // ignore corrupted files
                }
            }
            return list.OrderBy(t => t.Name);
        }

        public async Task<ThemeModel?> GetByIdAsync(string id)
        {
            var path = Path.Combine(_themesFolder, $"{id}.theme.json");
            if (!File.Exists(path)) return null;
            var text = await File.ReadAllTextAsync(path).ConfigureAwait(false);
            return JsonSerializer.Deserialize<ThemeModel>(text);
        }

        public async Task SaveAsync(ThemeModel theme)
        {
            var path = Path.Combine(_themesFolder, $"{theme.Id}.theme.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var text = JsonSerializer.Serialize(theme, options);
            await File.WriteAllTextAsync(path, text).ConfigureAwait(false);
        }

        public Task DeleteAsync(string id)
        {
            var path = Path.Combine(_themesFolder, $"{id}.theme.json");
            if (File.Exists(path)) File.Delete(path);
            return Task.CompletedTask;
        }
    }
}