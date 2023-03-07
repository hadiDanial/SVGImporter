using SVGImporter.Utility;
using System.Drawing;

namespace SVGImporter.Elements.PathUtility
{
    internal class CubicCurveCommand : PathCommand
    {
        private Vector2 controlPoint1, controlPoint2;
        private Vector2 point2;

        public Vector2 ControlPoint1 { get => controlPoint1; set => controlPoint1 = value; }
        public Vector2 ControlPoint2 { get => controlPoint2; set => controlPoint2 = value; }
        public Vector2 Point2 { get => point2; set => point2 = value; }
        
        public CubicCurveCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 6)
                throw new InvalidDataException($"Cubic curve command must have 6 values!");
            controlPoint1 = new Vector2(values[0], values[1]);
            controlPoint2 = new Vector2(values[2], values[3]);
            point2 = new Vector2(values[4], values[5]);
        }
        
        public CubicCurveCommand() { }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(CUBIC_CURVE)}{controlPoint1.x},{controlPoint1.y} {controlPoint2.x},{controlPoint2.y} {point2.x},{point2.y}";
        }
    }
}