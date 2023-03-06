using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    internal class Ellipse : Element
    {
        private Vector2 center;
        private Vector2 radius;

        protected Ellipse(string tagText, Vector2 center, Vector2 radius, List<TagAttribute> attributes) : base(tagText, attributes)
        {
            this.center = center;
            this.radius = radius;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName(TagType.Ellipse)} cx=\"{center.x}\" cy=\"{center.y}\" rx=\"{radius.x}\" ry=\"{radius.y}\" {AttributesToSVG()}/>";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {center}, R = {radius}";
        }


        public new static string GetElementNameReadable()
        {
            return "Ellipse";
        }

        public static new Ellipse GetElement(string tagText)
        {
            List<TagAttribute> attributes = TagAttribute.SVGToAttributes(tagText);
            float x = 0, y = 0, rx = 0, ry = 0;
            float val;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("cx") && float.TryParse(attribute.attributeValue, out val))
                    x = val;
                if (attribute.attributeName.Equals("cy") && float.TryParse(attribute.attributeValue, out val))
                    y = val;
                if (attribute.attributeName.Equals("rx") && float.TryParse(attribute.attributeValue, out val))
                    rx = val;
                if (attribute.attributeName.Equals("ry") && float.TryParse(attribute.attributeValue, out val))
                    ry = val;

            }
            if (rx == 0 && ry != 0) rx = ry;
            else if (ry == 0 && rx != 0) ry = rx;
            Vector2 center = new Vector2(x, y);
            Vector2 radius = new Vector2(rx, ry);

            Ellipse ellipse = new Ellipse(tagText, center, radius, attributes);
            return ellipse;
        }

        protected override TagType GetTagType()
        {
            return TagType.Ellipse;
        }
    }
}
