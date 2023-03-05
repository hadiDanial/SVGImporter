﻿using SVGImporter.Utility;

namespace SVGImporter.SVG.Elements
{
    internal class Polyline : Element
    {
        protected List<Vector2> points;

        public Polyline(string tagText, ViewBox viewBox, List<Attribute> attributes, List<Vector2> points) : base(tagText, viewBox, attributes)
        {
            this.points = points;
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName()} points=\"{Vector2.ToSVG(points)}\" {AttributesToSVG()} />";
        }

        public override string GetElementName()
        {
            return "polyline";
        }

        public override string GetElementNameReadable()
        {
            return "Polyline";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {points}";
        }
    }
}
