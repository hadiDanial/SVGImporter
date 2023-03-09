using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SVGImporter.Elements.Containers;
using SVGImporter.Elements.PathUtility;
using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    public class Path : Element
    {
        private List<PathCommand> pathCommands;
        private string pathData = string.Empty;
        private const string COMMAND_REGEX_PATTERN = "(([mzlhvcsqta]|[MZLHVCSQTA])( *)(\\d+\\.\\d+|\\d+)(([\n|\t|\r| |,|\\-])*(\\d+\\.\\d+|\\d+))*|z)";


        public static SVG CreatePathFromPoints(List<Vector2> points, Vector2 origin, bool useAbsolutePosition, bool isClosed)
        {
            List<PathCommand> commands = new List<PathCommand>();
            MoveCommand moveCommand = new MoveCommand();
            moveCommand.Point = points[0];
            moveCommand.IsAbsolute = useAbsolutePosition;
            commands.Add(moveCommand);
            for (int i = 1; i < points.Count; i += 4)
            {
                CubicCurveCommand cubicCurveCommand = new CubicCurveCommand();
                cubicCurveCommand.ControlPoint1 = points[i];
                cubicCurveCommand.ControlPoint2 = points[i + 1];
                cubicCurveCommand.Point2 = points[i + 2];
                commands.Add(cubicCurveCommand);
            }
            if (isClosed)
                commands.Add(new ClosePathCommand());
            Path path = new Path(commands);
            SVG svg = new SVG(new Vector2(100, 100), new ViewBox(new Vector2(100, 100), origin));
            Style style = new Style(new List<TagAttribute>());
            svg.SetChildren(new List<Element> { path, style });
            return svg;
        }
        internal Path(List<PathCommand> pathCommands) : base(new List<TagAttribute>())
        {
            this.pathCommands = pathCommands;
        }

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
            Regex regex = new Regex(COMMAND_REGEX_PATTERN);
            MatchCollection matches = regex.Matches(pathData);
            foreach (Match match in matches)
            {
                PathCommand command = PathCommand.CreateCommand(match.Value);
                pathCommands.Add(command);
            }
        }

        public string GetData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PathCommand command in pathCommands)
            {
                stringBuilder.Append(command.CommandToData());
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();
        }

        public override string ElementToSVGTag()
        {            
            string[] attributesToIgnore = { "d" };
            return $"<{GetElementName(GetTagType())} class=\"st0\" d=\"{GetData()}\" {AttributesToSVG(new List<string>(attributesToIgnore))}/>\n";
        }

        public new static string GetElementNameReadable()
        {
            return "Path";
        }

        public override string ToString()
        {
            return "Path: " + GetData();
        }

        protected override TagType GetTagType()
        {
            return TagType.Path;
        }
    }
}
