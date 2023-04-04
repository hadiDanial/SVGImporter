namespace SVGImporter.Elements.PathUtility
{
    public class ClosePathCommand : PathCommand
    {
        internal ClosePathCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
        }
        
        public ClosePathCommand() { }

        public override string CommandToData()
        {
            return CLOSE_PATH.ToString();
        }
    }
}