using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Elements;

namespace SVGImporter.Elements.Containers
{
    internal class Group : ParentElement
    {
        internal Group(List<TagAttribute> attributes) : base(attributes)
        {
        }

        public new static string GetElementNameReadable()
        {
            return "Group";
        }

        protected override string GetCustomData()
        {
            return String.Empty;
        }

        protected override TagType GetTagType()
        {
            return TagType.G;
        }
    }
}
