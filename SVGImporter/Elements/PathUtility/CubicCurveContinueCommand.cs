using SVGImporter.Utility;
using System.Numerics;

namespace SVGImporter.Elements.PathUtility
{
    internal class CubicCurveContinueCommand : PathCommand
    {
        private Vector2 controlPoint2;
        private Vector2 point2;

        public Vector2 ControlPoint2 { get => controlPoint2; set => controlPoint2 = value; }
        public Vector2 Point2 { get => point2; set => point2 = value; }

        public CubicCurveContinueCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 4)
                throw new SVGException($"Cubic curve continue command must have 4 values!");
            controlPoint2 = new Vector2(values[0], values[1]);
            point2 = new Vector2(values[2], values[3]);
        }
        
        public CubicCurveContinueCommand() { }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(CUBIC_CURVE_CONTINUE)}{controlPoint2.X},{controlPoint2.Y} {point2.X},{point2.Y}";
        }
    }
}