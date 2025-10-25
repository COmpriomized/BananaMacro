using System;
using System.IO;
using System.Runtime.Loader;
using System.Reflection;

namespace BananaMacro.Plugins.Loader
{
    public class PluginSandbox : AssemblyLoadContext, IDisposable
    {
        private readonly AssemblyDependencyResolver _resolver;

        public PluginSandbox(string pluginPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            var path = _resolver.ResolveAssemblyToPath(assemblyName);
            if (path != null) return LoadFromAssemblyPath(path);
            return null;
        }

        public Assembly LoadPluginAssembly(string pluginPath)
        {
            return LoadFromAssemblyPath(pluginPath);
        }

        public void UnloadAndCollect()
        {
            Unload();
            // Call GC to attempt collection; host should ensure no references remain
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void Dispose()
        {
            try { UnloadAndCollect(); } catch { }
        }
    }
}