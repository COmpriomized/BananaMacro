using System.Text;
using System.Collections.Generic;

namespace BananaMacro.SeedConfig
{
    public static class SeedHtmlRenderer
    {
        public static string RenderHtml(List<SeedModel> seeds)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><title>Seeds</title><style>");
            sb.AppendLine(".card { border:1px solid #ccc; padding:12px; margin:10px; border-radius:6px; }");
            sb.AppendLine(".title { font-weight:bold; font-size:1.2em; }");
            sb.AppendLine("</style></head><body><h1>Seed Cards</h1>");

            foreach (var seed in seeds)
            {
                sb.AppendLine("<div class='card'>");
                sb.AppendLine($"<div class='title'>{seed.Name}</div>");
                sb.AppendLine($"<div class='id'>ID: {seed.Id}</div>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("</body></html>");
            return sb.ToString();
        }
    }
}