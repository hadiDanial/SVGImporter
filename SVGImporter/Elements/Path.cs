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
        internal Path(List<TagAttribute> attributes) : base(attributes)
        {
        }

        public override string ElementToSVGTag()
        {
            return $"<{GetElementName(GetTagType())} {TagAttribute.AttributesToSVG(attributes)}/>\n";
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
