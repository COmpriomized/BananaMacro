# Plugin Security Guidance

Principles:
  - Treat all third-party plugins as untrusted code.
  - Prefer isolation using AssemblyLoadContext (PluginSandbox) to allow unloading.
  - Minimize privileges: do not automatically grant file system or network access.

Recommendations:
  - Validate plugin manifests before loading.
  - Run plugin code on limited threads and avoid executing arbitrary scripts.
  - Monitor plugin resource usage and provide UI controls to disable/unload plugins.
  - Log plugin load failures and exceptions to aid troubleshooting.

Operational controls:
  - Provide an "Enable/Disable" toggle in UI and persist state to Plugins/Metadata/plugins.json.
  - Consider running risky plugins in a separate process if stronger sandboxing is required.