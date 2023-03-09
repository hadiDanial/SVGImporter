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
            List<Element> elements = new List<Element>();
            MoveCommand moveCommand = new MoveCommand();
            Vector2 prevPoint = points[0];
            moveCommand.Point = points[0];
            moveCommand.IsAbsolute = useAbsolutePosition;
            commands.Add(moveCommand);
            for (int i = 1; i < points.Count; i += 3)
            {
                CubicCurveCommand cubicCurveCommand = new CubicCurveCommand();
                cubicCurveCommand.ControlPoint1 = points[i]+ prevPoint;
                cubicCurveCommand.ControlPoint2 = points[i + 2] - points[i + 1] ;
                // Control point locations:
                // Circle circle = (Circle)Element.CreateElement(TagType.Circle, new List<TagAttribute>(), "");
                // circle.Center = points[i];
                // circle.Radius = 10;
                // elements.Add(circle);
                // circle = (Circle)Element.CreateElement(TagType.Circle, new List<TagAttribute>(), "");
                // circle.Center = points[i+1];
                // circle.Radius = 10;
                // elements.Add(circle); 
                cubicCurveCommand.Point2 = points[i + 2];
                cubicCurveCommand.IsAbsolute = true;
                commands.Add(cubicCurveCommand);
            }
            if (isClosed)
                commands.Add(new ClosePathCommand());
            Path path = new Path(commands);
            SVG svg = new SVG(new Vector2(500, 500), new ViewBox(new Vector2(500, 500), new Vector2(-250, -250)));
            Style style = new Style(new List<TagAttribute>());
            elements.Add(path);
            elements.Add(style);
            svg.SetChildren(elements);
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
