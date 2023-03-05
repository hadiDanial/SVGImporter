using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Line:Element
    {
        private Vector2 point1, point2;

        public Line(string tagText, ViewBox viewBox, Vector2 point1, Vector2 point2, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} x1=\"{point1.x}\" y1=\"{point1.y}\" x2=\"{point2.x}\" y2=\"{point2.y}\" {AttributesToSVG()} />";
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
