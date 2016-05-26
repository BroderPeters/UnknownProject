using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledTileset
    {
        public TiledTileset()
        {
            Tiles = new List<TiledTile>();
        }

        [XmlAttribute(AttributeName = "firstgid")]
        public int FirstGid { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute(AttributeName = "tileheight")]
        public int TileHeight { get; set; }

        [XmlAttribute(AttributeName = "spacing")]
        public int Spacing { get; set; }

        [XmlAttribute(AttributeName = "margin")]
        public int Margin { get; set; }

        [XmlAttribute(AttributeName = "tilecount")]
        public int Tilecount { get; set; }

        [XmlAttribute(AttributeName = "columns")]
        public int Columns { get; set; }

        [XmlElement(ElementName = "tile")]
        public List<TiledTile> Tiles { get; set; }

        [XmlElement(ElementName = "image")]
        public TiledImage Image { get; set; }
    }
}
