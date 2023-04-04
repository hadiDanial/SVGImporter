using SVGImporter.Utility;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SVGImporter.Elements
{
    public class Polyline : Element
    {
        private List<Vector2> points;
        protected const string PATTERN = "([0-9]+)(\\.?)([0-9]*),([0-9]+)(\\.?)([0-9]*)";

        public List<Vector2> Points { get => points; set => points = value; }

        protected Polyline(List<TagAttribute> attributes, List<Vector2> points) : base(attributes)
        {
            this.points = points;
        }

        public Polyline(List<TagAttribute> attributes) : base(attributes)
        {
            string points = string.Empty;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("points"))
                    points = attribute.attributeValue;
            }
            List<Vector2> pointsList = new List<Vector2>();
            Regex regex = new Regex(PATTERN);
            MatchCollection matches = regex.Matches(points);
            string xStr, yStr;
            float x = 0, y = 0, val;
            foreach (Match match in matches)
            {
                string[] split = match.Value.Split(',');
                if (split == null || split.Length != 2) continue;
                xStr = split[0].Trim();
                yStr = split[1].Trim();
                if (float.TryParse(xStr, out val))
                    x = val;
                if (float.TryParse(yStr, out val))
                    y = val;
                Vector2 point = new Vector2(x, y);
                pointsList.Add(point);
            }
            this.points = pointsList;
        }

        public override string ElementToSVGTag()
        {
            string[] attributesToIgnore = { "points" };
            return $"<{GetElementName(TagType.Polyline)} points=\"{Vector2.ToSVG(points)}\" {AttributesToSVG(new List<string>(attributesToIgnore))} />\n";
        }

        public new static string GetElementNameReadable()
        {
            return "Polyline";
        }

        public override string ToString()
        {
            return $"{GetElementNameReadable()}: {points}";
        }

        public override TagType GetTagType()
        {
            return TagType.Polyline;
        }
    }
}
