using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Interfaces
{
    public interface ICloudSync : IDisposable
    {
        /// <summary>
        /// Provider id (eg: "onedrive", "dropbox", "s3")
        /// </summary>
        string ProviderId { get; }

        /// <summary>
        /// Initialize the sync client with provider-specific configuration
        /// </summary>
        Task InitializeAsync(IntegrationConfig config, CancellationToken ct = default);

        /// <summary>
        /// Push a local item (path) to cloud storage
        /// </summary>
        Task<bool> UploadAsync(string localPath, string remotePath, CancellationToken ct = default);

        /// <summary>
        /// Download a remote item to a local path
        /// </summary>
        Task<bool> DownloadAsync(string remotePath, string localPath, CancellationToken ct = default);

        /// <summary>
        /// List remote files or keys inside a remote folder
        /// </summary>
        Task<IEnumerable<string>> ListRemoteAsync(string remoteFolder, CancellationToken ct = default);

        /// <summary>
        /// Delete a remote item
        /// </summary>
        Task<bool> DeleteRemoteAsync(string remotePath, CancellationToken ct = default);

        /// <summary>
        /// Trigger a full sync (pull then push conflicts handled by implementation)
        /// </summary>
        Task<SyncResult> SyncAsync(CancellationToken ct = default);

        /// <summary>
        /// Optional event to report progress or discovered changes
        /// </summary>
        event Func<IntegrationEvent, Task>? OnSyncEvent;
    }

    public class SyncResult
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Details { get; set; }
        public string? ErrorMessage { get; set; }
    }
}