using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BananaMacro.Extensions.Interfaces;

namespace BananaMacro.Extensions.Stores
{
    public class FileStore : IStore
    {
        private readonly string _basePath;

        public FileStore(string basePath = null)
        {
            _basePath = string.IsNullOrWhiteSpace(basePath) ? Path.Combine(AppContext.BaseDirectory, "SavedData") : basePath;
            Directory.CreateDirectory(_basePath);
        }

        private string FullPath(string relative)
        {
            if (Path.IsPathRooted(relative)) return relative;
            return Path.Combine(_basePath, relative);
        }

        public async Task<T> LoadAsync<T>(string relativePath) where T : new()
        {
            var path = FullPath(relativePath);
            if (!File.Exists(path)) return new T();
            var json = await File.ReadAllTextAsync(path).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(json) ?? new T();
        }

        public async Task SaveAsync<T>(string relativePath, T data)
        {
            var path = FullPath(relativePath);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json).ConfigureAwait(false);
        }

        public Task<IEnumerable<string>> ListFilesAsync(string relativeFolder)
        {
            var folder = FullPath(relativeFolder);
            if (!Directory.Exists(folder)) return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());
            var files = Directory.GetFiles(folder);
            return Task.FromResult<IEnumerable<string>>(files);
        }
    }
}