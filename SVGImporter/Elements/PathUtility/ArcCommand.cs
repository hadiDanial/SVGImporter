using SVGImporter.Utility;

namespace SVGImporter.Elements.PathUtility
{
    internal class ArcCommand : PathCommand
    {
        private Vector2 radius, endPoint;
        private float rotation;
        private bool arc, sweep;

        public Vector2 Radius { get => radius; set => radius = value; }
        public Vector2 EndPoint { get => endPoint; set => endPoint = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public bool Arc { get => arc; set => arc = value; }
        public bool Sweep { get => sweep; set => sweep = value; }

        public ArcCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
            if(values == null || values.Count != 7)
                throw new InvalidDataException($"Arc command must have 7 values!");
            radius = new Vector2(values[0], values[1]);
            rotation = values[2];
            int arcInt = (int)values[3];
            int sweepInt = (int)values[4];
            if(arcInt < 0 || arcInt > 1 || sweepInt < 0 || sweepInt > 1)
                throw new InvalidDataException($"Arc and sweep values in Arc path command must be either 0 or 1!");
            arc = arcInt == 1;
            sweep = sweepInt == 1;
            endPoint = new Vector2(values[5], values[6]);
        }
        public override string CommandToData()
        {
            int arcInt = arc ? 1 : 0, sweepInt = sweep ? 1 : 0;
            return $"{GetCommandCharRelativeOrAbsolute(ARC)}{radius.x},{radius.y} , {rotation}, {arcInt},{sweepInt} , {endPoint.x},{endPoint.y}";
        }
    }
}