using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Group : ParentElement
    {
        public Group(string tagText, ViewBox viewBox, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {
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
