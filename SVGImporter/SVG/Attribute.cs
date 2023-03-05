using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVGImporter.SVG
{
    public struct Attribute
    {
        public string attributeName;
        public string attributeValue;
        private const string PATTERN = "[ ]([A-Z]|[a-z]|[0-9])+=\"([A-Z]|[a-z]|[0-9]|[ ])+\"";

        public Attribute(string attributeName, string attributeValue)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
        }

        public override string ToString()
        {
            return $"{attributeName}=\"{attributeValue}\"";
        }
        public static List<Attribute> SVGToAttributes(string tag)
        {
            List<Attribute> attributes = new List<Attribute>();
            Regex regex = new Regex(PATTERN);
            MatchCollection matches = regex.Matches(tag);
            foreach (Match match in matches)
            {
                Attribute attr = new Attribute();
                string[] split = match.Value.Split('=');
                if (split == null || split.Length != 2) continue;
                attr.attributeName = split[0].Trim();
                attr.attributeValue = split[1].Replace('\"',' ').Trim();
                attributes.Add(attr);
            }
            return attributes;
        }
        public static string AttributesToSVG(List<Attribute> attributes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Attribute attribute in attributes)
            {
                stringBuilder.Append(attribute);
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }
    }
}
