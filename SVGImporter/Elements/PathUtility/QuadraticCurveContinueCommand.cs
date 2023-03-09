using SVGImporter.Utility;
using System.Numerics;

namespace SVGImporter.Elements.PathUtility
{
    internal class QuadraticCurveContinueCommand : PathCommand
    {
        private Vector2 point2;

        public Vector2 Point2 { get => point2; set => point2 = value; }

        public QuadraticCurveContinueCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 4)
                throw new SVGException($"Quadric curve continue command must have 2 values!");
            point2 = new Vector2(values[0], values[1]);
        }

        public QuadraticCurveContinueCommand() { }

        public override string CommandToData()
        {
            return $"{GetCommandCharRelativeOrAbsolute(QUADRATIC_CURVE_CONTINUE)}{point2.X},{point2.Y}";
        }
    }
}