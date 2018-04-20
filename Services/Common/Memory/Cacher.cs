using System.Web.Caching;
using System.Web.Mvc;

namespace System.Web.Memory
{
    public static class InCache
    {
        public static T Get<T>(string cacheId) where T : class
        {
            return (T)HttpRuntime.Cache.Get(cacheId);
        }

        public static T Get<T>(string cacheId, T getItemCallback) where T : class
        {
            return Get(cacheId, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), getItemCallback);
        }

        public static T Get<T>(string cacheId, CacheDependency cacheDependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, T getItemCallback) where T : class
        {
            T _item = (T)HttpRuntime.Cache.Get(cacheId);
            HttpContext.Current.Cache.Remove(cacheId);
            HttpContext.Current.Cache.Insert(cacheId, Extensions.NullToString(getItemCallback), cacheDependency, absoluteExpiration, slidingExpiration);

            return getItemCallback;
        }
    }
}