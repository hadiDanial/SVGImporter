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

        public static new ParentElement GetElement(string tagText)
        {
            return null;
        }

        public new static string GetElementName()
        {
            return "g";
        }

        public new static string GetElementNameReadable()
        {
            return "Group";
        }
    }
}
