using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Library.Cache
{
    public static class CacheManager
    {
        private static Dictionary<string, object> m_memoryCache;
        private static Dictionary<string, object> MemoryCache
        {
            get { return m_memoryCache ?? (m_memoryCache = new Dictionary<string, object>()); }
        }

        /// <summary>
        /// Obtains an object from the cache
        /// </summary>
        /// <param name="cacheKey">The cache key</param>
        /// <returns>The value stored in the cache that matches the key provided</returns>
        public static object GetCacheValue(string cacheKey)
        {
            var cacheType = System.Configuration.ConfigurationManager.AppSettings["CachingType"];
            switch (cacheType)
            {
                case "ASPNetCaching":
                    return HttpRuntime.Cache.Get(cacheKey);;
                case "NoCaching":
                    return !MemoryCache.ContainsKey(cacheKey) ? null : MemoryCache[cacheKey];
                default:
                    return HttpRuntime.Cache.Get(cacheKey);
            }
        }

        /// <summary>
        /// Adds or updates a settings object to the cache
        /// </summary>
        /// <param name="cacheKey">the chache key</param>
        /// <param name="cacheValue">the cache value</param>
        public static void SetSettingCache(string cacheKey, object cacheValue)
        {
            // set this to 10 by default
            var cacheDuration = 10;
            try
            {
                cacheDuration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SettingsCacheDuration"]);
            }
            catch (Exception)
            {
                SetCache(cacheKey, cacheValue, 10);
                return;
            }
            SetCache(cacheKey, cacheValue, cacheDuration);
        }

        /// <summary>
        /// Adds or updates a settings object to the cache
        /// </summary>
        /// <param name="cacheKey">the chache key</param>
        /// <param name="cacheValue">the cache value</param>
        /// <param name="filePath">the cache dependency file path</param>
        public static void SetSettingCacheWithDependency(string cacheKey, object cacheValue, string filePath)
        {
            // set this to 10 by default
            var cacheDuration = 10;
            try
            {
                cacheDuration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SettingsCacheDuration"]);
            }
            catch (Exception)
            {
                SetCacheWithDependency(cacheKey, cacheValue, 10, filePath);
                return;
            }
            SetCacheWithDependency(cacheKey, cacheValue, cacheDuration, filePath);
        }

        /// <summary>
        /// Adds or updates an object into the cache
        /// </summary>
        /// <param name="cacheKey">The cache key</param>
        /// <param name="cacheValue">The cache value</param>
        /// <param name="cacheDuration">The cache duration</param>
        public static void SetCache(string cacheKey, object cacheValue, int cacheDuration)
        {
            var cacheType = System.Configuration.ConfigurationManager.AppSettings["CachingType"];
            switch (cacheType)
            {
                case "ASPNetCaching":
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, null, DateTime.Now.AddMinutes(cacheDuration), TimeSpan.Zero);
                    break;
                case "NoCaching":
                    if (!MemoryCache.ContainsKey(cacheKey))
                        MemoryCache.Add(cacheKey, cacheValue);
                    else
                        MemoryCache[cacheKey] = cacheValue;
                    break;
                default:
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, null, DateTime.Now.AddMinutes(cacheDuration), TimeSpan.Zero);
                    break;
            }
        }

        /// <summary>
        /// Adds or updates an object into the cache
        /// </summary>
        /// <param name="cacheKey">the chache key</param>
        /// <param name="cacheValue">the cache value</param>
        /// <param name="cacheDuration">The cache duration</param>
        /// <param name="filePath">the cache dependency file path</param>
        public static void SetCacheWithDependency(string cacheKey, object cacheValue, int cacheDuration, string filePath)
        {
            var cacheType = System.Configuration.ConfigurationManager.AppSettings["CachingType"];
            switch (cacheType)
            {
                case "ASPNetCaching":
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, new CacheDependency(filePath), DateTime.Now.AddMinutes(cacheDuration), TimeSpan.Zero);
                    break;
                case "NoCaching":
                    if (!MemoryCache.ContainsKey(cacheKey))
                        MemoryCache.Add(cacheKey, cacheValue);
                    else
                        MemoryCache[cacheKey] = cacheValue;
                    break;
                default:
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, new CacheDependency(filePath), DateTime.Now.AddMinutes(cacheDuration), TimeSpan.Zero);
                    break;
            }
        }

        /// <summary>
        /// Clears the cache for the given cache key
        /// </summary>
        /// <param name="cacheKey">The cache key to be cleared</param>
        public static void ClearCache(string cacheKey)
        {
            if (HttpRuntime.Cache.Get(cacheKey) != null)
                HttpRuntime.Cache.Remove(cacheKey);
        }
    }
}
