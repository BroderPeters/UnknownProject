using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownProject.Core;
using UnknownProject.Core.Configurations;
using UnknownProject.Engine;
using UnknownProject.Engine.Components;

namespace UnknownProject.Components.Maps
{
    public class DesertMapComponent : MapComponent
    {
        public DesertMapComponent(Func<PartMapComponent> partMapProvider, Camera cam, GraphicConfiguration graphic) : base(partMapProvider, cam, graphic)
        {
            MapNames = new String[1,1] {
                { "test/desert2" }
            };
        }
    }
}
