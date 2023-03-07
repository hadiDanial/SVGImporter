using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.Elements.Containers
{
    public class Style : ParentElement
    {
        public Style(List<TagAttribute> attributes) : base(attributes)
        {
        }
        protected override TagType GetTagType()
        {
            return TagType.Style;
        }
    }
}
