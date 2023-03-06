using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Line : Element
    {
        private Vector2 point1, point2;

        public Line(string tagText, Vector2 point1, Vector2 point2, List<Attribute> attributes) : base(tagText, attributes)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} x1=\"{point1.x}\" y1=\"{point1.y}\" x2=\"{point2.x}\" y2=\"{point2.y}\" {AttributesToSVG()} />";
        }

        public static new Line GetElement(string tagText)
        {
            List<Attribute> attributes = Attribute.SVGToAttributes(tagText);
            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            float val;
            foreach (Attribute attribute in attributes)
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

            Line line = new Line(tagText, null, p1, p2, attributes);
            return line;
        }

        public override string GetElementName()
        {
            return "line";
        }

        public override string GetElementNameReadable()
        {
            return "Line";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {point1}, {point2}";
        }
    }
}
