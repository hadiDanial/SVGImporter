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
            return $"<{GetElementName(GetTagType())} {TagAttribute.AttributesToSVG(attributes)}/>\n";
        }

        public static new Path GetElement(string tagText)
        {
            return new Path(tagText, new List<TagAttribute>());
            //throw new NotImplementedException();
        }

        public new static string GetElementNameReadable()
        {
            return "Path";
        }

        public override string ToString()
        {
            return "Path-NotImplemented";
            throw new NotImplementedException();
        }

        protected override TagType GetTagType()
        {
            return TagType.Path;
        }
    }
}
