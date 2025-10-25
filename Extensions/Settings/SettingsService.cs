using System;
using System.Threading.Tasks;
using BananaMacro.Extensions.Interfaces;
using BananaMacro.Models.Entities;

namespace BananaMacro.Extensions.Settings
{
    public class SettingsService
    {
        private readonly IStore _store;
        public AppSettings Settings { get; private set; }

        private const string SettingsFile = "config.json";

        public SettingsService(IStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            Settings = new AppSettings { Theme = "LightBlack", PreferredFont = "Space Mono" };
        }

        public async Task LoadAsync()
        {
            Settings = await _store.LoadAsync<AppSettings>(SettingsFile).ConfigureAwait(false) ?? new AppSettings();
        }

        public async Task SaveAsync()
        {
            await _store.SaveAsync(SettingsFile, Settings).ConfigureAwait(false);
        }
    }
}