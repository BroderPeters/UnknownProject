using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledObjectGroup
    {
        public TiledObjectGroup()
        {
            Objects = new List<TiledObject>();
        }

        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlElement(ElementName = "object")]
        public List<TiledObject> Objects { get; set; }
    }
}
