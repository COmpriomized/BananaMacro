using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BananaMacro.Extensions.Logging;

namespace BananaMacro.Extensions.Plugins
{
    public class PluginManager
    {
        private readonly string _pluginFolder;
        private readonly LoggingService _log;
        private readonly List<IBananaPlugin> _loaded = new List<IBananaPlugin>();

        public IReadOnlyList<IBananaPlugin> LoadedPlugins => _loaded.AsReadOnly();

        public PluginManager(LoggingService log, string pluginFolder = null)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _pluginFolder = string.IsNullOrWhiteSpace(pluginFolder) ? Path.Combine(AppContext.BaseDirectory, "Plugins") : pluginFolder;
            Directory.CreateDirectory(_pluginFolder);
        }

        public void DiscoverAndLoad(IServiceProvider services = null)
        {
            var dlls = Directory.GetFiles(_pluginFolder, "*.dll");
            foreach (var dll in dlls)
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    var types = asm.GetTypes().Where(t => typeof(IBananaPlugin).IsAssignableFrom(t) && !t.IsAbstract);
                    foreach (var t in types)
                    {
                        try
                        {
                            var inst = (IBananaPlugin)Activator.CreateInstance(t);
                            inst.Initialize(services);
                            _loaded.Add(inst);
                            _log.Append(new Models.Entities.LogEntry { Level = "Info", Source = "PluginManager", Message = $"Loaded plugin {inst.Name} ({inst.Version}) from {Path.GetFileName(dll)}" });
                        }
                        catch (Exception ex)
                        {
                            _log.Append(new Models.Entities.LogEntry { Level = "Error", Source = "PluginManager", Message = $"Failed to create plugin instance from {dll}: {ex.Message}" });
                        }
                    }
                }
                catch (ReflectionTypeLoadException rtle)
                {
                    _log.Append(new Models.Entities.LogEntry { Level = "Error", Source = "PluginManager", Message = $"Failed to load assembly {dll}: {rtle.Message}" });
                }
                catch (Exception ex)
                {
                    _log.Append(new Models.Entities.LogEntry { Level = "Error", Source = "PluginManager", Message = $"Failed to load {dll}: {ex.Message}" });
                }
            }
        }

        public void UnloadAll()
        {
            foreach (var p in _loaded.ToArray())
            {
                try
                {
                    p.Dispose();
                }
                catch { }
            }
            _loaded.Clear();
            _log.Append(new Models.Entities.LogEntry { Level = "Info", Source = "PluginManager", Message = "All plugins unloaded" });
        }
    }
}