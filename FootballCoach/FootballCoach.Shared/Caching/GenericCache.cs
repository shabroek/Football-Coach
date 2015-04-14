using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Isah.Core.Caching
{
    public class GenericCache<T> : IGenericCache<T>
    {
        public TimeSpan CacheDuration
        {
            get;
            set;
        }

        private readonly TimeSpan _defaultCacheDurationInMinutes = TimeSpan.FromMinutes(30);

        private readonly MemoryCache _cache;
        private readonly Func<string, Task<T>> _updateValueFunc;

        public GenericCache()
        {
            CacheDuration = _defaultCacheDurationInMinutes;
            _cache = InitCache();

        }

        public GenericCache(TimeSpan duration)
        {
            CacheDuration = duration;
            _cache = InitCache();

        }

        public GenericCache(TimeSpan duration, Func<string, Task<T>> updateValueFunc)
        {
            CacheDuration = duration;
            _cache = InitCache();
            _updateValueFunc = updateValueFunc;
        }

        private MemoryCache InitCache()
        {
            var config = new NameValueCollection();
            return new MemoryCache(typeof(T).ToString(), config);

        }

        [ExcludeFromCodeCoverage]
        public bool Get(string key, out T value)
        {
            try
            {
                if (_cache[key] == null)
                {
                    if (_updateValueFunc == null)
                    {
                        value = default(T);
                        return false;
                    }
                    value = _updateValueFunc(key).Result;
                    Set(key, value, CacheDuration);
                    return true;
                }

                value = (T)_cache[key];
            }
            catch
            {
                value = default(T);
                return false;
            }

            return true;
        }

        public void Set(string key, T value)
        {
            Set(key, value, CacheDuration);
        }

        public void Set(string key, T value, TimeSpan duration)
        {
            _cache.Set(key, value, duration == TimeSpan.MaxValue ? DateTime.MaxValue : DateTime.Now.AddMinutes(duration.TotalMinutes));
        }

        public void Clear(string key)
        {
            _cache.Remove(key);
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _cache.Select(item => new KeyValuePair<string, object>(item.Key, item.Value));
        }

        public void ClearAll()
        {
            foreach (var item in GetAll())
            {
                _cache.Remove(item.Key);
            }
        }

        public void Clear(Func<KeyValuePair<string, object>, bool> predicate)
        {
            foreach (var item in GetAll().Where(predicate))
            {
                _cache.Remove(item.Key);
            }
        }

    }
}
