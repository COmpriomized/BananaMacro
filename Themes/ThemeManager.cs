using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BananaMacro.Themes
{
    public class ThemeManager : IDisposable
    {
        private readonly IThemeProvider _provider;
        private readonly List<ThemeModel> _cache = new();

        public ThemeModel? ActiveTheme { get; private set; }

        public event Action<ThemeModel?>? OnThemeChanged;

        public ThemeManager(IThemeProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task InitializeAsync()
        {
            var list = await _provider.GetAvailableAsync().ConfigureAwait(false);
            _cache.Clear();
            _cache.AddRange(list);
            ActiveTheme = _cache.FirstOrDefault();
        }

        public IEnumerable<ThemeModel> GetAll() => _cache.ToArray();

        public async Task<ThemeModel?> SetActiveAsync(string id)
        {
            var theme = _cache.FirstOrDefault(t => t.Id == id) ?? await _provider.GetByIdAsync(id);
            if (theme == null) return null;
            ActiveTheme = theme;
            OnThemeChanged?.Invoke(theme);
            return theme;
        }

        public async Task AddOrUpdateAsync(ThemeModel theme)
        {
            await _provider.SaveAsync(theme).ConfigureAwait(false);
            var existing = _cache.FirstOrDefault(t => t.Id == theme.Id);
            if (existing != null) _cache.Remove(existing);
            _cache.Add(theme);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            await _provider.DeleteAsync(id).ConfigureAwait(false);
            var existing = _cache.FirstOrDefault(t => t.Id == id);
            if (existing != null) _cache.Remove(existing);
            if (ActiveTheme?.Id == id) ActiveTheme = _cache.FirstOrDefault();
            OnThemeChanged?.Invoke(ActiveTheme);
            return true;
        }

        public void Dispose()
        {
            OnThemeChanged = null;
            _cache.Clear();
        }
    }
}