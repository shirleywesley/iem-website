namespace Library.Cache
{
    public static class SettingsCache
    {
        private const string WebSettingsCacheKey = "WEB_SETTINGS_CACHE_KEY";

        /// <summary>
        /// Retrieves the websettings object
        /// </summary>
        /// <returns>Web settings configuration object</returns>
        public static WebSettings WebSettings
        {
            get
            {
                var obj = CacheManager.GetCacheValue(WebSettingsCacheKey);
                if (obj == null)
                {
                    var webSettings = new WebSettings();
                    CacheManager.SetSettingCache(WebSettingsCacheKey, webSettings);
                    return webSettings;
                }
                return (WebSettings)CacheManager.GetCacheValue(WebSettingsCacheKey);
            }
        }
    }
}
