using SVGImporter.Utility;
using System.Drawing;

namespace SVGImporter.Elements.PathUtility
{
    internal class QuadraticCurveCommand : PathCommand
    {
        private Vector2 controlPoint;
        private Vector2 point2;

        public Vector2 ControlPoint { get => controlPoint; set => controlPoint = value; }
        public Vector2 Point2 { get => point2; set => point2 = value; }

        public QuadraticCurveCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 4)
                throw new InvalidDataException($"Quadric curve command must have 4 values!");
            controlPoint = new Vector2(values[0], values[1]);
            point2 = new Vector2(values[2], values[3]);
        }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(QUADRATIC_CURVE)}{controlPoint.x},{controlPoint.y} {point2.x},{point2.y}";
        }
    }
}