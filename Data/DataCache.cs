using System;
using System.Collections.Concurrent;

namespace BananaMacro.Data
{
    public class DataCache<T>
    {
        private class CacheEntry
        {
            public T Value { get; set; }
            public DateTime Expiry { get; set; }
        }

        private readonly ConcurrentDictionary<string, CacheEntry> _cache = new();

        public void Set(string key, T value, TimeSpan ttl)
        {
            _cache[key] = new CacheEntry { Value = value, Expiry = DateTime.UtcNow + ttl };
        }

        public bool TryGet(string key, out T? value)
        {
            value = default;
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.Expiry > DateTime.UtcNow)
                {
                    value = entry.Value;
                    return true;
                }
                _cache.TryRemove(key, out _);
            }
            return false;
        }

        public void Clear() => _cache.Clear();
    }
}