using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SVGImporter.Elements;
using SVGImporter.Elements.PathUtility;

namespace SVGImporter.Elements
{
    internal class Path : Element
    {
        private List<PathCommand> pathCommands;
        private string pathData = string.Empty;
        private bool isClosedPath;
        private const string COMMAND_REGEX_PATTERN = "(([mzlhvcsqta]|[MZLHVCSQTA])( *)(\\d+\\.\\d+|\\d+)(([\n|\t|\r| |,|\\-])*(\\d+\\.\\d+|\\d+))*|z)";

        public bool IsClosedPath { get => isClosedPath; set => isClosedPath = value; }

        internal Path(List<TagAttribute> attributes) : base(attributes)
        {
            pathCommands = new List<PathCommand>();
            IsClosedPath = false;
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
            Regex regex = new Regex(COMMAND_REGEX_PATTERN);
            MatchCollection matches = regex.Matches(pathData);
            foreach (Match match in matches)
            {
                PathCommand command = PathCommand.CreateCommand(match.Value);
                if (command == null) continue; // TODO: Remove, this is only here because nothing is implemented yet
                pathCommands.Add(command);
            }
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
