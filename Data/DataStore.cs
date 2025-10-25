using System.Collections.Generic;

namespace BananaMacro.Data
{
    public class DataStore<T>
    {
        private readonly Dictionary<string, T> _store = new();

        public void Set(string key, T value) => _store[key] = value;

        public bool TryGet(string key, out T? value) => _store.TryGetValue(key, out value);

        public bool Remove(string key) => _store.Remove(key);

        public IEnumerable<string> Keys => _store.Keys;

        public IEnumerable<T> Values => _store.Values;

        public void Clear() => _store.Clear();
    }
}