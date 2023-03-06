using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Elements;

namespace SVGImporter.Elements
{
    internal class Path : Element
    {
        protected Path(string tagText, List<TagAttribute> attributes) : base(tagText, attributes)
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

        public new static string GetElementName()
        {
            return "path";
        }

        public new static string GetElementNameReadable()
        {
            return "Path";
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
