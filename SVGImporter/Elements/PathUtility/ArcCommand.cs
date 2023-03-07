namespace SVGImporter.Elements.PathUtility
{
    internal class ArcCommand : PathCommand
    {
        public ArcCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
        }

        public override string CommandToData()
        {
            return "ARC";
        }
    }
}