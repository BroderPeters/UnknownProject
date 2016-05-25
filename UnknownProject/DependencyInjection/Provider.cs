using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.DependencyInjection
{
    public class Provider<T>
    {
        public T Get()
        {
            return (T)DependencyInjectionContainer.Get().GetInstance(typeof(T));
        }
    }
}
