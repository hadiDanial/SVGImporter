using SVGImporter.Utility;

namespace SVGImporter.SVG.Elements
{
    internal class Polygon : Polyline
    {
        public Polygon(string tagText, ViewBox viewBox, List<Attribute> attributes, List<Vector2> points) : base(tagText, viewBox, attributes, points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points");
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
