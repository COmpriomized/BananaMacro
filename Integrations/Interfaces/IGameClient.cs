using System;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.GameService.Models;

namespace BananaMacro.Integrations.Interfaces
{
    public interface IGameClient : IDisposable
    {
        /// <summary>
        /// Human-friendly id for the game client implementation
        /// </summary>
        string Id { get; }

        /// <summary>
        /// True when the client has an active connection to the game
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Initialize the client with optional settings
        /// </summary>
        Task InitializeAsync(string? settingsJson = null, CancellationToken ct = default);

        /// <summary>
        /// Connect to the game or start reading state
        /// </summary>
        Task ConnectAsync(CancellationToken ct = default);

        /// <summary>
        /// Disconnect or stop reading state
        /// </summary>
        Task DisconnectAsync(CancellationToken ct = default);

        /// <summary>
        /// Returns the last known game state (may be cached)
        /// </summary>
        GameState? GetCurrentState();

        /// <summary>
        /// Request a snapshot read of the game state
        /// </summary>
        Task<GameState?> ReadStateAsync(CancellationToken ct = default);

        /// <summary>
        /// Event raised whenever the client observes a new game state
        /// </summary>
        event Func<GameState, Task>? OnStateUpdated;

        /// <summary>
        /// Event raised for raw messages, chat lines, or client-specific events
        /// </summary>
        event Func<string, Task>? OnRawEvent;
    }
}