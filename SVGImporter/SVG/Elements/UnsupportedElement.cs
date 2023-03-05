using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class UnsupportedElement : Group
    {
        public UnsupportedElement(string tagText, ViewBox viewBox, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {
        }
    }
}
