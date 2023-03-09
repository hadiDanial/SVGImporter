using System.Collections.Generic;

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

        protected override TagType GetTagType()
        {
            return TagType.G;
        }
    }
}
