namespace SVGImporter.Elements.PathUtility
{
    internal class ClosePathCommand : PathCommand
    {
        private ClosePathCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
        }

        public static PathCommand INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new ClosePathCommand(string.Empty, false);
                return instance;
            }
            set
            {
                INSTANCE = value;
            }
        }

        private static ClosePathCommand instance = null;
    }
}