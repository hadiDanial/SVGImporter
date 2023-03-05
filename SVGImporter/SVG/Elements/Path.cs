using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    internal class Path : Element
    {
        public Path(string tagText, ViewBox viewBox, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {
        }

        public override string ElementToSVGTag()
        {
            throw new NotImplementedException();
        }

        public static new Path GetElement(string tagText)
        {
            throw new NotImplementedException();
        }

        public override string GetElementName()
        {
            throw new NotImplementedException();
        }

        public override string GetElementNameReadable()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
