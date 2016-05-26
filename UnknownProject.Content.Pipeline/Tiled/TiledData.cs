using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledData
    {

        [XmlText]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "encoding")]
        public string Encoding { get; set; }

        [XmlAttribute(AttributeName = "compression")]
        public string Compression { get; set; }

        [XmlIgnore]
        public uint[,] Tiles { get; set; }
    }
}