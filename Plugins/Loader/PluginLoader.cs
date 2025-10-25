using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BananaMacro.Extensions.Plugins;
using BananaMacro.Extensions.Logging;
using BananaMacro.Models.Entities;

namespace BananaMacro.Plugins.Loader
{
    public static class PluginLoader
    {
        public static IBananaPlugin? LoadFromPath(string dllPath, LoggingService? log = null, IServiceProvider? services = null)
        {
            if (!File.Exists(dllPath))
            {
                log?.Append(new LogEntry { Level = "Warn", Source = "PluginLoader", Message = $"DLL not found: {dllPath}" });
                return null;
            }

            try
            {
                var asm = Assembly.LoadFrom(dllPath);
                var pluginTypes = asm.GetTypes().Where(t => typeof(IBananaPlugin).IsAssignableFrom(t) && !t.IsAbstract);
                foreach (var t in pluginTypes)
                {
                    try
                    {
                        var inst = (IBananaPlugin?)Activator.CreateInstance(t);
                        if (inst == null) continue;
                        inst.Initialize(services);
                        log?.Append(new LogEntry { Level = "Info", Source = "PluginLoader", Message = $"Loaded plugin {inst.Name} ({inst.Version}) from {Path.GetFileName(dllPath)}" });
                        return inst;
                    }
                    catch (Exception ex)
                    {
                        log?.Append(new LogEntry { Level = "Error", Source = "PluginLoader", Message = $"Failed to create plugin instance from {dllPath}: {ex.Message}" });
                    }
                }

                log?.Append(new LogEntry { Level = "Warn", Source = "PluginLoader", Message = $"No IBananaPlugin implementation found in {dllPath}" });
            }
            catch (ReflectionTypeLoadException rtle)
            {
                var msg = string.Join("; ", rtle.LoaderExceptions?.Select(e => e.Message) ?? Array.Empty<string>());
                log?.Append(new LogEntry { Level = "Error", Source = "PluginLoader", Message = $"ReflectionTypeLoadException loading {dllPath}: {msg}" });
            }
            catch (Exception ex)
            {
                log?.Append(new LogEntry { Level = "Error", Source = "PluginLoader", Message = $"Exception loading {dllPath}: {ex.Message}" });
            }

            return null;
        }
    }
}