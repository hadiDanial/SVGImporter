using SVGImporter.Utility;
using System.Collections.Generic;


namespace SVGImporter.Elements
{
    internal class Line : Element
    {
        private Vector2 point1, point2;

        protected Line(Vector2 point1, Vector2 point2, List<TagAttribute> attributes) : base(attributes)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Line(List<TagAttribute> attributes) : base(attributes)
        {
            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            float val;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("x1") && float.TryParse(attribute.attributeValue, out val))
                    x1 = val;
                if (attribute.attributeName.Equals("y1") && float.TryParse(attribute.attributeValue, out val))
                    y1 = val;
                if (attribute.attributeName.Equals("x2") && float.TryParse(attribute.attributeValue, out val))
                    x2 = val;
                if (attribute.attributeName.Equals("y2") && float.TryParse(attribute.attributeValue, out val))
                    y2 = val;
            }
            this.point1 = new Vector2(x1, y1);
            this.point2 = new Vector2(x2, y2);
        }

        public override string ElementToSVGTag()
        {
            string[] attributesToIgnore = { "x1", "y1", "x2", "y2" };
            return $"<{GetElementName(TagType.Line)} x1=\"{point1.x}\" y1=\"{point1.y}\" x2=\"{point2.x}\" y2=\"{point2.y}\" {AttributesToSVG(new List<string>(attributesToIgnore))}/>\n";
        }      

        public new static string GetElementNameReadable()
        {
            return "Line";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {point1}, {point2}";
        }

        protected override TagType GetTagType()
        {
            return TagType.Line;
        }
    }
}
