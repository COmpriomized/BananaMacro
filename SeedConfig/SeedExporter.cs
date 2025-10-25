using System.IO;
using System.Threading.Tasks;

namespace BananaMacro.SeedConfig
{
    public static class SeedExporter
    {
        public static async Task ExportToHtmlAsync(string jsonPath, string htmlPath)
        {
            var seeds = await SeedLoader.LoadAsync(jsonPath);
            var html = SeedHtmlRenderer.RenderHtml(seeds);
            await File.WriteAllTextAsync(htmlPath, html);
        }
    }
}