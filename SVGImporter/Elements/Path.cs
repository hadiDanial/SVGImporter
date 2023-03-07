using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Elements;
using SVGImporter.Elements.PathUtility;

namespace SVGImporter.Elements
{
    internal class Path : Element
    {
        private List<PathCommand> pathCommands;
        private string pathData = string.Empty;
        internal Path(List<TagAttribute> attributes) : base(attributes)
        {
            pathCommands = new List<PathCommand>();
            foreach (var attribute in attributes)
            {
                if (attribute.attributeName.Equals("d"))
                    pathData = attribute.attributeValue;
            }
            ParseData();
        }

        private void ParseData()
        {
            if(string.IsNullOrEmpty(pathData))
                return;
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
        }

        protected override TagType GetTagType()
        {
            return TagType.Path;
        }
    }
}
