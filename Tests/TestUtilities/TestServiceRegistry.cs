using System;
using System.Collections.Concurrent;

namespace BananaMacro.Tests.Utilities
{
    public class TestServiceRegistry : IDisposable
    {
        private readonly ConcurrentDictionary<Type, object> _services = new ConcurrentDictionary<Type, object>();

        public void Register<T>(T instance) where T : class => _services[typeof(T)] = instance;
        public T? Get<T>() where T : class => _services.TryGetValue(typeof(T), out var o) ? o as T : null;
        public void Dispose() => _services.Clear();
    }
}