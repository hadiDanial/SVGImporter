
namespace SVGImporter.Elements.PathUtility
{
    internal class SimpleMoveCommand : PathCommand
    {
        private float value;
        private SimpleMoveType movementType;

        public float Value { get => value; set => this.value = value; }
        internal SimpleMoveType MovementType { get => movementType; set => movementType = value; }

        public SimpleMoveCommand(string data, bool isAbsolute, SimpleMoveType movementType) : base(data, isAbsolute)
        {
            if (values == null || values.Count != 1)
                throw new InvalidDataException($"{movementType} command must have one value!");
            value = values[0];
            this.movementType = movementType;
        }
        public override string CommandToData()
        {
            if (MovementType == SimpleMoveType.Horizontal)
                return $"{GetCommandCharRelativeOrAbsolute(HORIZONTAL)}{value}";
            else
                return $"{GetCommandCharRelativeOrAbsolute(VERTICAL)}{value}";
        }
        internal enum SimpleMoveType
        {
            Horizontal, Vertical
        }
    }
}