using System;
using System.Collections.Generic;

namespace Isah.Core.Caching
{
    public interface IGenericCache<T>
    {
        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if
        /// item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        bool Get(string key, out T value);

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="value">Item to be cached</param>
        /// <param name="key">Name of item</param>
        void Set(string key, T value);

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs WITH a cache duration set in minutes
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Item to be cached</param>
        /// <param name="value">Name of item</param>
        /// <param name="duration">Cache duration</param>
        void Set(string key, T value, TimeSpan duration);

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        void Clear(string key);

        void Clear(Func<KeyValuePair<string, object>, bool> predicate);

        IEnumerable<KeyValuePair<string, object>> GetAll();

        void ClearAll();
    }

}
