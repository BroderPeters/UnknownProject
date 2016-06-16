using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Core.Configurations
{
    [DependencyInjection.Singleton]
    public class GraphicConfiguration
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
