using System;
using System.Collections.Concurrent;
using BananaMacro.Extensions.Interfaces;

namespace BananaMacro.Extensions
{
    public static class ServiceRegistry
    {
        private static readonly ConcurrentDictionary<Type, object> _services = new ConcurrentDictionary<Type, object>();

        public static void Register<T>(T instance) where T : class
        {
            _services.AddOrUpdate(typeof(T), instance, (_, __) => instance);
        }

        public static T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var obj)) return obj as T;
            return null;
        }

        public static void Clear() => _services.Clear();
    }
}