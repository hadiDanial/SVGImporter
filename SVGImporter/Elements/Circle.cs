﻿using SVGImporter.Utility;


namespace SVGImporter.Elements
{
    internal class Circle : Element
    {
        private Vector2 center;
        private float radius;

        protected Circle(string tagText, Vector2 center, float radius, List<TagAttribute> attributes) : base(tagText, attributes)
        {
            this.center = center;
            this.radius = radius;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} cx=\"{center.x}\" cy=\"{center.y}\" r=\"{radius}\" {AttributesToSVG()}/>";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {center}, R = {radius}";
        }

        public new static string GetElementName()
        {
            return "circle";
        }

        public new static string GetElementNameReadable()
        {
            return "Circle";
        }

        public static new Circle GetElement(string tagText)
        {
            List<TagAttribute> attributes = TagAttribute.SVGToAttributes(tagText);
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
            Vector2 center = new Vector2(x, y);

            Circle circle = new Circle(tagText, center, r, attributes);
            return circle;
        }
    }
}
