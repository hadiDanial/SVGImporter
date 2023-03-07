﻿namespace SVGImporter.Elements.PathUtility
{
    internal class ClosePathCommand : PathCommand
    {
        internal ClosePathCommand(string data, bool isAbsolute) : base(data, isAbsolute)
        {
        }

        public override string CommandToData()
        {
            return CLOSE_PATH.ToString();
        }
    }
}