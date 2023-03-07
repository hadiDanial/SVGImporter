using SVGImporter.Utility;


namespace SVGImporter.Elements
{
    internal class Circle : Element
    {
        private Vector2 center;
        private float radius;

        protected Circle(Vector2 center, float radius, List<TagAttribute> attributes) : base(attributes)
        {
            this.center = center;
            this.radius = radius;
        }

        public Circle(List<TagAttribute> attributes) : base(attributes)
        {
            float x = 0, y = 0, r = 0;
            float val;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("cx") && float.TryParse(attribute.attributeValue, out val))
                    x = val;
                if (attribute.attributeName.Equals("cy") && float.TryParse(attribute.attributeValue, out val))
                    y = val;
                if (attribute.attributeName.Equals("r") && float.TryParse(attribute.attributeValue, out val))
                    r = val;
            }
            this.center = new Vector2(x, y);
            this.radius = r;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName(TagType.Circle)} cx=\"{center.x}\" cy=\"{center.y}\" r=\"{radius}\" {AttributesToSVG()}/>\n";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {center}, R = {radius}";
        }

        public new static string GetElementNameReadable()
        {
            return "Circle";
        }

        protected override TagType GetTagType()
        {
            return TagType.Circle;
        }
    }
}
