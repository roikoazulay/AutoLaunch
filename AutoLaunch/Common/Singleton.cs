using System;
using System.Collections.Generic;

namespace AutomationCommon
{
    public class Singleton
    {
        private static readonly Dictionary<string, object> singletons = new Dictionary<string, object>();

        public static T Instance<T>()
           where T : class
        {
            return Instance<T>(typeof(T).FullName);
        }

        public static T Instance<T>(string name)
           where T : class
        {
            T result;
            if (singletons.ContainsKey(name))
            {
                result = (T)singletons[name];
            }
            else
            {
                result = (T)Activator.CreateInstance(typeof(T), true);
                singletons[name] = result;
            }

            return result;
        }

        /// <summary>
        /// Gets instance of type T for the specifed name.
        /// If not exist - create one using T's empty constuctor.
        /// </summary>
        /// <typeparam name="T">The singleton type.</typeparam>
        /// <param name="name">The name param.</param>
        /// <param name="getNewInstance">The get new instance.</param>
        /// <returns>the singleton instance.</returns>
        public static T Instance<T>(string name, Func<T> getNewInstance) where T : class
        {
            T result;
            if (singletons.ContainsKey(name))
            {
                result = (T)singletons[name];
            }
            else
            {
                result = getNewInstance();
                singletons[name] = result;
            }

            return result;
        }

        /// <summary>
        /// Clears the instance for <c>T</c>
        /// </summary>
        /// <typeparam name="T">the singleton type.</typeparam>
        public static void Clear<T>()
            where T : class
        {
            Clear(typeof(T).FullName);
        }

        public static void Clear(string name)
        {
            singletons.Remove(name);
        }

        /// <summary>
        /// Clears all instances
        /// </summary>
        public static void ClearAll()
        {
            singletons.Clear();
        }
    }
}