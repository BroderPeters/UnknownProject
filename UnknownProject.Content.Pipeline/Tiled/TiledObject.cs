using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnknownProject.Content.Pipeline.Tiled
{
    public class TiledObject
    {
        public TiledObject()
        {
            Properties = new List<TiledProperty>();
        }

        [XmlElement(ElementName = "name")]
        public String Name { get; set; }

        [XmlAttribute(DataType = "int", AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(DataType = "float", AttributeName = "x")]
        public float X { get; set; }

        [XmlAttribute(DataType = "float", AttributeName = "y")]
        public float Y { get; set; }

        [XmlAttribute(DataType = "float", AttributeName = "width")]
        public float Width { get; set; }

        [XmlAttribute(DataType = "float", AttributeName = "height")]
        public float Height { get; set; }

        [XmlElement(ElementName = "ellipse")]
        public TiledEllipse Ellipse { get; set; }

        [XmlElement(ElementName = "polygon")]
        public TiledPolygon Polygon { get; set; }

        [XmlElement(ElementName = "polyline")]
        public TiledPolyline Polyline { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> Properties { get; set; }
    }

}
