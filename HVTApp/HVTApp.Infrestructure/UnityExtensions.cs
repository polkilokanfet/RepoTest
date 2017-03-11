using System;
using Microsoft.Practices.Unity;

namespace HVTApp.Infrastructure
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Registers an object for navigation.
        /// </summary>
        /// <typeparam name="T">The Type of the object to register</typeparam>
        /// <param name="container"><see cref="IUnityContainer"/> used to register type for Navigation.</param>
        public static IUnityContainer RegisterViewForNavigation<T>(this IUnityContainer container)
        {
            Type type = typeof(T);
            string viewName = type.FullName;
            return container.RegisterType(typeof(object), type, viewName);
        }
    }
}
