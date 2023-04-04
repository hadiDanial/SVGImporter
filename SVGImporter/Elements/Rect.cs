using SVGImporter.Utility;
using System.Collections.Generic; 

namespace SVGImporter.Elements
{
    public class Rect : Element
    {
        private Vector2 position;
        private Vector2 size;
        private Vector2 cornerRadius;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Size { get => size; set => size = value; }
        public Vector2 CornerRadius { get => cornerRadius; set => cornerRadius = value; }

        protected Rect(List<TagAttribute> attributes, Vector2 position, Vector2 size, Vector2 cornerRadius) : base(attributes)
        {
            this.position = position;
            this.size = size;
            this.cornerRadius = cornerRadius;
        }

        public Rect(List<TagAttribute> attributes) : base(attributes)
        {
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
            this.position = new Vector2(x, y);
            this.size = new Vector2(width, height);
            this.cornerRadius = new Vector2(rx, ry);

        }

        public override string ElementToSVGTag()
        {
            string[] attributesToIgnore = { "x", "y", "rx", "ry", "width", "height" };
            return $"<{GetElementName(TagType.Rect)} x=\"{position.x}\" y=\"{position.y}\" " +
                $"width=\"{size.x}\" height=\"{size.y}\" rx=\"{cornerRadius.x}\" ry=\"{cornerRadius.y}\" {AttributesToSVG(new List<string>(attributesToIgnore))}/>\n";
        }


        public override string ToString()
        {
            return $"Rect: {position}, size = {size}, corners = {cornerRadius}";
        }

        public new static string GetElementNameReadable()
        {
            return "Rect";
        }

        public override TagType GetTagType()
        {
            return TagType.Rect;
        }
    }
}
