using System;
using BananaMacro.Extensions.Plugins;

namespace SamplePlugin
{
    public class SamplePluginImpl : IBananaPlugin
    {
        public string Id => "com.example.sampleplugin";
        public string Name => "Sample Plugin";
        public string Version => "1.0.0";

        private IServiceProvider? _services;

        public void Initialize(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            var logger = services.GetService(typeof(BananaMacro.Extensions.Logging.LoggingService)) as BananaMacro.Extensions.Logging.LoggingService;
            logger?.Append(new BananaMacro.Models.Entities.LogEntry { Level = "Info", Source = "SamplePlugin", Message = "SamplePlugin initialized" });
        }

        public void Dispose()
        {
            var logger = _services?.GetService(typeof(BananaMacro.Extensions.Logging.LoggingService)) as BananaMacro.Extensions.Logging.LoggingService;
            logger?.Append(new BananaMacro.Models.Entities.LogEntry { Level = "Info", Source = "SamplePlugin", Message = "SamplePlugin disposed" });
            _services = null;
        }
    }
}