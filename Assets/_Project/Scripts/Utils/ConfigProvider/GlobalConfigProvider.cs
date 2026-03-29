using System;
using System.Collections.Generic;
using System.Linq;

public class GlobalConfigProvider : IConfigProvider
{
    private readonly Dictionary<Type, object> _cache;

    public GlobalConfigProvider(ConfigRegistry registry)
    {
        _cache = registry.Configs
            .Where(c => c != null)
            .ToDictionary(c => c.GetType(), c => (object)c);
    }

    public T Get<T>() where T : class
    {
        if (_cache.TryGetValue(typeof(T), out var config))
        {
            return (T)config;
        }

        throw new Exception($"[ConfigProvider] Config of type {typeof(T).Name} not found in Registry!");
    }
}