using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.Elements.Containers
{
    public class Style : ParentElement
    {
        private const string DEFAULT_STYLE = ".st0{fill:none;stroke:#000000;stroke-miterlimit:10;}";
        public Style(List<TagAttribute> attributes) : base(attributes)
        {
            value = DEFAULT_STYLE;
        }

        protected override string GetCustomData()
        {
            return "type=\"text/css\"";
        }

        protected override TagType GetTagType()
        {
            return TagType.Style;
        }
    }
}
