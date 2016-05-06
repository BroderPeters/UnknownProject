using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.DependencyInjection
{
    /// <summary>
    /// Instructs the <see cref="DependencyInjectionContainer"/> to not create new instances from the given class and reuse the old.
    /// Example:
    /// <code>
    /// public SomeConstructor(Someclass obj1, Someclass obj2) 
    /// {
    ///     Assert.IsTrue(obj1 == obj2); // its the same instance.
    /// }
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Singleton : Attribute { }
}
