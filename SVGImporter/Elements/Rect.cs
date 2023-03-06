using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    internal class Rect : Element
    {
        private Vector2 position;
        private Vector2 size;
        private Vector2 cornerRadius;

        protected Rect(string tagText, List<TagAttribute> attributes, Vector2 position, Vector2 size, Vector2 cornerRadius) : base(tagText, attributes)
        {
            this.position = position;
            this.size = size;
            this.cornerRadius = cornerRadius;
        }


        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} x=\"{position.x}\" y=\"{position.y}\" " +
                $"width=\"{size.x}\" height=\"{size.y}\" rx=\"{cornerRadius.x}\" ry=\"{cornerRadius.y}\" {TagAttribute.AttributesToSVG(attributes)}/>";
        }

        public new static string GetElementName()
        {
            return "rect";
        }

        public override string ToString()
        {
            return $"Rect: {position}, size = {size}, corners = {cornerRadius}";
        }

        public new static string GetElementNameReadable()
        {
            return "Rect";
        }

        public static new Rect GetElement(string tagText)
        {
            List<TagAttribute> attributes = TagAttribute.SVGToAttributes(tagText);
            float x = 0, y = 0, width = 0, height = 0, rx = 0, ry = 0;
            float val;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("x") && float.TryParse(attribute.attributeValue, out val))
                    x = val;
                if (attribute.attributeName.Equals("y") && float.TryParse(attribute.attributeValue, out val))
                    y = val;
                if (attribute.attributeName.Equals("width") && float.TryParse(attribute.attributeValue, out val))
                    width = val;
                if (attribute.attributeName.Equals("height") && float.TryParse(attribute.attributeValue, out val))
                    height = val;
                if (attribute.attributeName.Equals("rx") && float.TryParse(attribute.attributeValue, out val))
                    rx = val;
                if (attribute.attributeName.Equals("ry") && float.TryParse(attribute.attributeValue, out val))
                    ry = val;
            }
            Vector2 position = new Vector2(x, y);
            Vector2 size = new Vector2(width, height);
            Vector2 radius = new Vector2(rx, ry);

            Rect rect = new Rect(tagText, attributes, position, size, radius);
            return rect;
        }
    }
}
