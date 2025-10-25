using System.Collections.Generic;

namespace BananaMacro.Themes
{
    public class ThemeModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDark { get; set; } = false;
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Fonts { get; set; } = new();
        public Dictionary<string, string> Extras { get; set; } = new();
    }
}