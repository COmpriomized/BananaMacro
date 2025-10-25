using System.IO;
using System.Threading.Tasks;

namespace BananaMacro.GearConfig
{
    public static class GearExporter
    {
        public static async Task ExportToHtmlAsync(string jsonPath, string htmlPath)
        {
            var gears = await GearLoader.LoadAsync(jsonPath);
            var html = GearHtmlRenderer.RenderHtml(gears);
            await File.WriteAllTextAsync(htmlPath, html);
        }
    }
}