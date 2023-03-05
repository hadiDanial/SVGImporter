using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Ellipse : Element
    {
        private Vector2 center;
        private Vector2 radius;

        public Ellipse(string tagText, ViewBox viewBox, Vector2 center, Vector2 radius, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {  
            this.center = center;
            this.radius = radius;           
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} cx=\"{center.x}\" cy=\"{center.y}\" rx=\"{radius.x}\" ry=\"{radius.y}\" {AttributesToSVG()}/>";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {center}, R = {radius}";
        }

        public override string GetElementName()
        {
            return "ellipse";
        }

        public override string GetElementNameReadable()
        {
            return "Ellipse";
        }
    }
}
