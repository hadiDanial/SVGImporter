﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVGImporter.Elements
{
    public struct TagAttribute
    {
        public string attributeName;
        public string attributeValue;
        private const string PATTERN = "[ ]([A-Z]|[a-z]|[0-9])+=\"([A-Z]|[a-z]|[0-9]|[ ])+\"";

        public TagAttribute(string attributeName, string attributeValue)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
        }

        public override string ToString()
        {
            return $"{attributeName}=\"{attributeValue}\"";
        }
        public static List<TagAttribute> SVGToAttributes(string tag)
        {
            List<TagAttribute> attributes = new List<TagAttribute>();
            Regex regex = new Regex(PATTERN);
            MatchCollection matches = regex.Matches(tag);
            foreach (Match match in matches)
            {
                TagAttribute attr = new TagAttribute();
                string[] split = match.Value.Split('=');
                if (split == null || split.Length != 2) continue;
                attr.attributeName = split[0].Trim();
                attr.attributeValue = split[1].Replace('\"', ' ').Trim();
                attributes.Add(attr);
            }
            return attributes;
        }
        public static string AttributesToSVG(List<TagAttribute> attributes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TagAttribute attribute in attributes)
            {
                stringBuilder.Append(attribute);
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }
    }
}