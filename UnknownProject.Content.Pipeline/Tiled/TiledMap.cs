using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    [XmlRoot(ElementName = "map")]
    public class TiledMap
    {
        public TiledMap(int height, int width, int tileHeight, int tileWidth)
        {
            Height = height;
            Width = width;
            TileHeight = tileHeight;
            TileWidth = tileWidth;
            Layers = new List<TiledLayer>();
            ObjectGroups = new List<TiledObjectGroup>();
            Tilesets = new List<TiledTileset>();
        }

        public TiledMap()
        {
            Layers = new List<TiledLayer>();
            ObjectGroups = new List<TiledObjectGroup>();
            Tilesets = new List<TiledTileset>();
        }

        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }

        [XmlAttribute(AttributeName = "tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute(AttributeName = "tileheight")]
        public int TileHeight { get; set; }

        [XmlElement(ElementName = "tileset")]
        public List<TiledTileset> Tilesets { get; set; }

        [XmlElement(ElementName = "objectgroup")]
        public List<TiledObjectGroup> ObjectGroups { get; set; }

        [XmlElement(ElementName = "layer")]
        public List<TiledLayer> Layers { get; set; }
    }
}
