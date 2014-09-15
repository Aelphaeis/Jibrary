using System;

namespace Jibrary.Miscellaneous
{
    public sealed class Singleton<T>
    {
        /// <summary>
        /// The Call by which we find the instance of the class wrapped by the Singleton.
        /// </summary>
        public static T Instance
        {
            get 
            {
                if (!instantiated && (typeof(T).IsValueType || typeof(T).GetConstructor(new Type[0]) != null))
                {
                    instance = (T)Activator.CreateInstance(typeof(T));
                    instantiated = true;
                }

                if (instantiated)
                    return instance;

                throw new InvalidOperationException("Instance has not been instantiated");
            } 
        }

        /// <summary>
        /// The Singleton's, by creating it here and only exposing a Get property we effectively make this read only once it has been instiantaited. 
        /// </summary>
        internal static T instance;

        /// <summary>
        /// Used to check if the singleton's Instance has been assigned yet
        /// </summary>
        static bool instantiated;


        /// <summary>
        /// This allows us to manually set the value of Instance if it has not been set yet.
        /// </summary>
        /// <param name="inst">The Instance we would like to use for the singleton</param>
        /// <returns>The instance assigned to the Singleton</returns>
        public static T Instantiate(T inst)
        {
            if (instantiated)
                throw new InvalidOperationException("A Singleton may only be Instantiated once");

            instance = inst;
            instantiated = true;
            return instance;
        }

        /// <summary>
        /// This prevents this called from being Instantiated
        /// </summary>
        internal Singleton() { }
    }
}
