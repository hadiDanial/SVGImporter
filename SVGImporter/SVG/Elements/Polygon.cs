using SVGImporter.Utility;

namespace SVGImporter.SVG.Elements
{
    internal class Polygon : Polyline
    {
        public Polygon(string tagText, List<Attribute> attributes, List<Vector2> points) : base(tagText, attributes, points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points");
        }

        public static new Polygon GetElement(string tagText)
        {
            throw new NotImplementedException();
        }

        public override string GetElementName()
        {
            return "polygon";
        }

        public override string GetElementNameReadable()
        {
            return "Polygon";
        }
    }
}
