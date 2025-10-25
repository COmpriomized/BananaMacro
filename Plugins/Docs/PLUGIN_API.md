# BananaMacro Plugin API

Interface: IBananaPlugin
  - string Id { get; }
  - string Name { get; }
  - string Version { get; }
  - void Initialize(IServiceProvider services)
  - void Dispose()

Host services available in Initialize:
  - BananaMacro.Extensions.Logging.LoggingService
  - BananaMacro.Extensions.Settings.SettingsService
  - BananaMacro.Extensions.Macro.MacroEngine
  - ServiceRegistry (via IServiceProvider.GetService)

Threading model:
  - Initialize and Dispose are called on the host thread that loaded the plugin.
  - Plugins may spawn background tasks but must manage cancellation and dispose cleanly.

Best practices:
  - Keep plugin dependencies minimal and target net6.0 or netstandard2.0 for broader compatibility.
  - Use logging service for diagnostics instead of writing files directly.
  - Call Dispose to free resources and unregister handlers.