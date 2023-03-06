using SVGImporter.Elements;
using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    internal class Polygon : Polyline
    {
        protected Polygon(string tagText, List<TagAttribute> attributes, List<Vector2> points) : base(tagText, attributes, points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points");
        }

        public static new Polygon GetElement(string tagText)
        {
            Polygon polygon = (Polygon)Polyline.GetElement(tagText);
            if (polygon.points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points");
            return polygon;
        }

        public new static string GetElementName()
        {
            return "polygon";
        }

        public new static string GetElementNameReadable()
        {
            return "Polygon";
        }
    }
}
