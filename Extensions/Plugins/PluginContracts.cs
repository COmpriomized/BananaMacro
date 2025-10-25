using System;

namespace BananaMacro.Extensions.Plugins
{
    public interface IBananaPlugin : IDisposable
    {
        string Id { get; }
        string Name { get; }
        string Version { get; }
        void Initialize(IServiceProvider services);
    }
}