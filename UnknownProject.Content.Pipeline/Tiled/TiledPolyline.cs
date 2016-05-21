using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledPolyline
    {
        [XmlAttribute(AttributeName = "points")]
        public string Points { get; set; }
    }
}