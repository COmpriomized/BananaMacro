using System.Text;
using System.Collections.Generic;

namespace BananaMacro.GearConfig
{
    public static class GearHtmlRenderer
    {
        public static string RenderHtml(List<GearModel> gears)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><title>Gears</title><style>");
            sb.AppendLine(".card { border:1px solid #ccc; padding:12px; margin:10px; border-radius:6px; }");
            sb.AppendLine(".title { font-weight:bold; font-size:1.2em; }");
            sb.AppendLine("</style></head><body><h1>Gear Cards</h1>");

            foreach (var gear in gears)
            {
                sb.AppendLine("<div class='card'>");
                sb.AppendLine($"<div class='title'>{gear.Name}</div>");
                sb.AppendLine($"<div class='id'>ID: {gear.Id}</div>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("</body></html>");
            return sb.ToString();
        }
    }
}