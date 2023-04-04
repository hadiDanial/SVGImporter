using SVGImporter.Utility;

namespace SVGImporter.Elements.PathUtility
{
    public class MoveCommand : PathCommand
    {
        private Vector2 point;
        public Vector2 Point { get => point; set => point = value; }

        public MoveCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 2)
                throw new SVGException($"Move command must have 2 values!");
            point = new Vector2(values[0], values[1]);
        }
        
        public MoveCommand() { }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(MOVE)}{point.x},{point.y}";
        }
    }
}