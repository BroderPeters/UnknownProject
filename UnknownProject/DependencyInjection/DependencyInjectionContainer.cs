using System;
using LightInject;
using System.Reflection;

namespace UnknownProject.DependencyInjection
{
    /// <summary>
    /// A container which contains the <see cref="ServiceContainer"/> from LightInject, that this is accessable in the whole project.
    /// </summary>
    static class DependencyInjectionContainer
    {
        private static readonly ServiceContainer Container;
        private static readonly Type SingletonType = typeof(Singleton);
       
        /// <summary>
        /// Scan the assembly and register.
        /// </summary>
        static DependencyInjectionContainer()
        {
            var assembly = typeof(DependencyInjectionContainer).Assembly;
            Container = new ServiceContainer();
            Container.RegisterAssembly(assembly);
            RegisterSingletons(assembly);
        }

        /// <summary>
        /// Scan the given assembly for types with the attribute <see cref="Singleton" />.
        /// </summary>
        /// <param name="assembly">The assembly were the singleton's should get registered</param>
        private static void RegisterSingletons(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(SingletonType, true).Length > 0)
                {
                    Container.Register(type, new PerContainerLifetime());
                }
            }
        }

        /// <returns>The current initialised <see cref="ServiceContainer"/></returns>
        public static ServiceContainer Get()
        {
            return Container;
        }
    }
}
