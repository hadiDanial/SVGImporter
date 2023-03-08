using SVGImporter.Utility;

namespace SVGImporter.Elements.PathUtility
{
    internal class LineCommand : PathCommand
    {
        private Vector2 point;
        public Vector2 Point { get => point; set => point = value; }

        public LineCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 2)
                throw new SVGException($"Line command must have 2 values!");
            point = new Vector2(values[0], values[1]);
        }
        
        public LineCommand() { }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(LINE)}{point.x},{point.y}";
        }
    }
}