
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
                throw new InvalidDataException($"Line command must have 2 values!");
            value = values[0];
            this.movementType = movementType;
        }

        internal enum SimpleMoveType
        {
            Horizontal, Vertical
        }
    }
}