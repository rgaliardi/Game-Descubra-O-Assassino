using System.Reflection;

namespace System.Web.Mvc
{
    /// <summary>
    /// Class that can be derived from to create a singleton. the only issue is that the type must have a non-public parameterless constructor.
    /// </summary>
    /// <typeparam name="T">Any type inherited from Singleton&lt;T&gt;></typeparam>
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        /// Lazily created instance Common.
        /// </summary>
        private readonly static Lazy<T> _instance;

        /// <summary>
        /// Lazily created instance property.
        /// </summary>
        public static T Instance
        {
            get { return _instance.Value; }
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Singleton()
        {
            _instance = new Lazy<T>(InstanceFactory);
        }

        /// <summary>
        /// Calls a non-public empty constructor on derived type to create instance. If no such constructor exists a TypeAccessException is thrown.
        /// </summary>
        /// <returns></returns>
        private static T InstanceFactory()
        {
            var _type = typeof(T);
            var _constructors = _type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (_constructors.Length == 1)
            {
                var _ctor = _type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

                // Make sure we found our one and only private or protected constructor.
                if ((_ctor != null) && (_ctor.IsPrivate || _ctor.IsFamily))
                {
                    var _instance = _ctor.Invoke(new object[] { }) as T;

                    if (_instance == null)
                    {
                        throw new TypeInitializationException(_type.FullName, new NullReferenceException());
                    }

                    return _instance;
                }
            }
            throw new TypeInitializationException(_type.FullName, new TypeAccessException("Type must contain a single (non-public) constructor if derived from Singleton<T>."));
        }        
    }
}