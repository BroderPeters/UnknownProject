using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledTile
    {
        public TiledTile()
        {
            ObjectGroups = new List<TiledObjectGroup>();
        }

        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "objectgroup")]
        public List<TiledObjectGroup> ObjectGroups { get; set; }
    }
}
