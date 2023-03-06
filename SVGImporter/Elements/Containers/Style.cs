using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.Elements.Containers
{
    public class Style : ParentElement
    {
        public Style(string tagText, List<TagAttribute> attributes) : base(tagText, attributes)
        {
        }
        public static new Style GetElement(string tagText)
        {
            List<TagAttribute> attributes;
            string content, value;
            GetContentAndAttributes(tagText, GetElementName(TagType.Style), TagType.G, out attributes, out content, out value);

            Style style = new Style(tagText, attributes);
            style.children = Element.GetElements(content);
            style.value = value;
            Console.WriteLine("Content:\n" + content);
            return style;
        }

        protected override TagType GetTagType()
        {
            return TagType.Style;
        }
    }
}
