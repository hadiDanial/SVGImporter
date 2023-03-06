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
        protected Group(string tagText, List<TagAttribute> attributes) : base(tagText, attributes)
        {
        }

        public static new Group GetElement(string tagText)
        {
            List<TagAttribute> attributes;
            string content, value;
            GetContentAndAttributes(tagText, GetElementName(TagType.G), TagType.G, out attributes, out content, out value);

            Group group = new Group(tagText, attributes);
            group.children = Element.GetElements(content);
            group.value = value;
            Console.WriteLine("Content:\n" + content);
            return group;
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
