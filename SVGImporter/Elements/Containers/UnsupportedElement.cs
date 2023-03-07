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

        public static new UnsupportedElement GetElement(string tagText)
        {
            List<TagAttribute> attributes;
            string content, value;
            GetContentAndAttributes(tagText, GetElementName(TagType.Unknown), TagType.Unknown, out attributes, out content, out value);
            UnsupportedElement svg = new UnsupportedElement(attributes);
            svg.children = Element.GetElements(content);
            Console.WriteLine("Content:\n" + content);
            return svg;
        }
    }
}
