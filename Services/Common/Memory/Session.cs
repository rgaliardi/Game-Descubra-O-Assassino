namespace System.Web.Memory
{
    public static class InSession
    {
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static T Entity<T>(string itemName, T getItemCallback, bool clear = false) where T : class
        {
            if (clear)
            {
                HttpContext.Current.Session[itemName] = null;
            }
            T _item = (T)HttpContext.Current.Session[itemName];

            if (_item == null)
            {
                HttpContext.Current.Session[itemName] = getItemCallback;
                _item = getItemCallback;
            }
            return _item;
        }

        public static T Entity<T>(string itemName, bool clear = false) where T : class
        {
            if (clear)
            {
                HttpContext.Current.Session[itemName] = null;
            }
            T _item = (T)HttpContext.Current.Session[itemName];
            return _item;
        }

        public static T Scope<T>(string itemName, bool clear = false) where T : class
        {
            if (clear)
            {
                HttpContext.Current.Session[itemName] = null;
            }
            T _item = (T)HttpContext.Current.Session[itemName];
            return _item;
        }

        public static T Scope<T>(string itemName, T getItemCallback) where T : class
        {
            T _item = (T)HttpContext.Current.Session[itemName];

            if (_item == null)
            {
                HttpContext.Current.Session[itemName] = getItemCallback;
                _item = getItemCallback;
            }
            return _item;
        }
    }
}