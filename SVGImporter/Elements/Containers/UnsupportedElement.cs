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
        internal UnsupportedElement(List<TagAttribute> attributes) : base(attributes)
        {
        }
        protected override TagType GetTagType()
        {
            return TagType.Unknown;
        }
    }
}
