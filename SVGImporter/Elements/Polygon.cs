using SVGImporter.Elements;
using SVGImporter.Utility;
using System.Collections.Generic;

namespace SVGImporter.Elements
{
    internal class Polygon : Polyline
    {
        protected Polygon(List<TagAttribute> attributes, List<Vector2> points) : base(attributes, points)
        {
            if (points.Count < 3)
                throw new SVGException("Polygon must have at least three points");
        }

        public Polygon(List<TagAttribute> attributes) : base(attributes)
        {
        }

        public override string ElementToSVGTag()
        {
            string[] attributesToIgnore = { "points" };
            return $"<{GetElementName(TagType.Polygon)} points=\"{Vector2.ToSVG(points)}\" {AttributesToSVG(new List<string>(attributesToIgnore))} />\n";
        }
        public new static string GetElementNameReadable()
        {
            return "Polygon";
        }
        protected override TagType GetTagType()
        {
            return TagType.Polygon;
        }
    }
}
