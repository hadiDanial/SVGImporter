using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Circle : Element
    {
        private Vector2 center;
        private float radius;

        public Circle(string tagText, ViewBox viewBox, Vector2 center, float radius, List<Attribute> attributes) : base(tagText, viewBox, attributes)
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

        public override string GetElementName()
        {
            return "circle";
        }

        public override string GetElementNameReadable()
        {
            return "Circle";
        }

        public static new Circle GetElement(string tagText)
        {
            throw new NotImplementedException();
        }
    }
}
