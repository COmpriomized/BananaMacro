using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;
using BananaMacro.Extensions.Interfaces;

namespace BananaMacro.Tests.Utilities
{
    public class InMemoryStore : IStore
    {
        private readonly ConcurrentDictionary<string, string> _storage = new ConcurrentDictionary<string, string>();

        public Task<T> LoadAsync<T>(string relativePath) where T : new()
        {
            if (_storage.TryGetValue(relativePath, out var json))
            {
                var obj = JsonSerializer.Deserialize<T>(json);
                return Task.FromResult(obj ?? new T());
            }
            return Task.FromResult(new T());
        }

        public Task SaveAsync<T>(string relativePath, T data)
        {
            var json = JsonSerializer.Serialize(data);
            _storage[relativePath] = json;
            return Task.CompletedTask;
        }

        public Task<System.Collections.Generic.IEnumerable<string>> ListFilesAsync(string relativeFolder)
        {
            return Task.FromResult<System.Collections.Generic.IEnumerable<string>>(_storage.Keys);
        }
    }
}