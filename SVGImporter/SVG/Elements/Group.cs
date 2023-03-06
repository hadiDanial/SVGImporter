using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Group : ParentElement
    {
        public Group(string tagText, List<Attribute> attributes) : base(tagText, attributes)
        {
        }

        public static new ParentElement GetElement(string tagText)
        {
            return null;
        }

        public override string GetElementName()
        {
            return "g";
        }

        public override string GetElementNameReadable()
        {            
            return "Group";
        }
    }
}
