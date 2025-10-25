using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Interfaces
{
    public interface IIntegration : IDisposable
    {
        string Id { get; }
        string Name { get; }
        IntegrationStatus Status { get; }
        Task InitializeAsync(IntegrationConfig config, CancellationToken ct = default);
        Task StartAsync(CancellationToken ct = default);
        Task StopAsync(CancellationToken ct = default);
        event Func<IntegrationEvent, Task>? OnEvent;
    }
}