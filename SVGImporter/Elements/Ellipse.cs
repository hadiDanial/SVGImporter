using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    internal class Ellipse : Element
    {
        private Vector2 center;
        private Vector2 radius;

        protected Ellipse(Vector2 center, Vector2 radius, List<TagAttribute> attributes) : base(attributes)        {
            this.center = center;
            this.radius = radius;
        }

        public Ellipse(List<TagAttribute> attributes) : base(attributes)
        {
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
            this.center = new Vector2(x, y);
            this.radius = new Vector2(rx, ry);
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName(TagType.Ellipse)} cx=\"{center.x}\" cy=\"{center.y}\" rx=\"{radius.x}\" ry=\"{radius.y}\" {AttributesToSVG()}/>\n";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {center}, R = {radius}";
        }


        public new static string GetElementNameReadable()
        {
            return "Ellipse";
        }

        protected override TagType GetTagType()
        {
            return TagType.Ellipse;
        }
    }
}
