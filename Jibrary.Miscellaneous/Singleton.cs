using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Jibrary.Miscellaneous
{
    public sealed class Singleton<T>
    {
        public static T Instance
        {
            get 
            {
                if (!instantiated &&( typeof(T).GetConstructor(new Type[0]) != null))
                {
                    Int32 r = new Int32();
                    instance = (T)Activator.CreateInstance(typeof(T));
                    instantiated = true;
                }

                if (instantiated)
                    return instance;


                throw new InvalidOperationException("Instance has not been instantiated");
            } 
        }

        internal static T instance;
        static bool instantiated;

        /// <summary>
        /// This will allow classes with default constructors to be instiantated immediately;
        /// </summary>
        //static Singleton()
        //{
        //    //If the class has the default constructor then crate it
        //    { 
        //        instantiated = true;
        //    }
        //    //If not the class instance implicitly be default(T)
        //}

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
