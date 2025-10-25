using System.Collections.Generic;
using BananaMacro.Models.Entities;

namespace BananaMacro.Models.Persistence
{
    public class PersistedCollections
    {
        public List<MacroDefinition> Macros { get; set; } = new List<MacroDefinition>();
        public List<SeedDefinition> Seeds { get; set; } = new List<SeedDefinition>();
        public List<GearConfig> Gears { get; set; } = new List<GearConfig>();
        public List<PluginInfo> Plugins { get; set; } = new List<PluginInfo>();
        public AppSettings Settings { get; set; } = new AppSettings();
    }
}