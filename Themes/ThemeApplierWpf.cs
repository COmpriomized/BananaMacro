using System;
using System.Windows;
using System.Windows.Media;

namespace BananaMacro.Themes.Adapters
{
    public static class ThemeApplierWpf
    {
        public static void ApplyTheme(ThemeModel theme)
        {
            if (theme == null) return;
            var app = Application.Current;
            if (app == null) return;

            // Apply color resources
            foreach (var kv in theme.Colors)
            {
                try
                {
                    var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kv.Value));
                    brush.Freeze();
                    if (app.Resources.Contains(kv.Key)) app.Resources[kv.Key] = brush;
                    else app.Resources.Add(kv.Key, brush);
                }
                catch
                {
                    // ignore invalid color strings
                }
            }

            // Apply fonts or extras as simple strings in resources
            foreach (var kv in theme.Fonts)
            {
                if (app.Resources.Contains(kv.Key)) app.Resources[kv.Key] = kv.Value;
                else app.Resources.Add(kv.Key, kv.Value);
            }

            foreach (var kv in theme.Extras)
            {
                if (app.Resources.Contains(kv.Key)) app.Resources[kv.Key] = kv.Value;
                else app.Resources.Add(kv.Key, kv.Value);
            }

            // Optional: set window chrome or styles by switching ResourceDictionary if present
        }
    }
}