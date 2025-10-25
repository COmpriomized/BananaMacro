# SamplePlugin

Build:
  dotnet build

Usage:
  - Copy the built SamplePlugin.dll to Plugins/Installed/
  - Optionally place a matching plugin.manifest.json next to the DLL
  - Restart BananaMacro or call PluginManager.DiscoverAndLoad to load the plugin

Notes:
  - Implements IBananaPlugin; Initialize receives an IServiceProvider with host services.
  - Use ServiceRegistry or resolved LoggingService to emit logs.