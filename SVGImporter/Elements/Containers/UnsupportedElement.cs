using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Elements;

namespace SVGImporter.Elements.Containers
{
    internal class UnsupportedElement : Group
    {
        protected UnsupportedElement(string tagText, List<TagAttribute> attributes) : base(tagText, attributes)
        {
        }
    }
}
