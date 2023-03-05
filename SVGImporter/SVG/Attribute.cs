using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG
{
    public struct Attribute
    {
        public string attributeName;
        public string attributeValue;

        public Attribute(string attributeName, string attributeValue)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
        }

        public override string ToString()
        {
            return $"{attributeName}=\"{attributeValue}\"";
        }
    }
}
