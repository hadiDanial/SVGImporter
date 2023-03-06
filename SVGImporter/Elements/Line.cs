using SVGImporter.Utility;


namespace SVGImporter.Elements
{
    internal class Line : Element
    {
        private Vector2 point1, point2;

        protected Line(string tagText, Vector2 point1, Vector2 point2, List<TagAttribute> attributes) : base(tagText, attributes)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName(TagType.Line)} x1=\"{point1.x}\" y1=\"{point1.y}\" x2=\"{point2.x}\" y2=\"{point2.y}\" {AttributesToSVG()}/>\n";
        }

        public static new Line GetElement(string tagText)
        {
            List<TagAttribute> attributes = TagAttribute.SVGToAttributes(tagText);
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
            Vector2 p1 = new Vector2(x1, y1);
            Vector2 p2 = new Vector2(x2, y2);

            Line line = new Line(tagText, p1, p2, attributes);
            return line;
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
