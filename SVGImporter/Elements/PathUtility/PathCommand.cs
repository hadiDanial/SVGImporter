using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.Elements.PathUtility
{
    internal abstract class PathCommand
    {
        private bool isAbsolute;

        private const char MOVE = 'M';
        private const char LINE = 'L';
        private const char HORIZONTAL = 'H';
        private const char VERTICAL = 'V';
        private const char END = 'Z';
        private const char CUBIC_CURVE = 'C';
        private const char CUBIC_CURVE_CONTINUE = 'S';
        private const char QUADRATIC_CURVE = 'Q';
        private const char QUADRATIC_CURVE_CONTINUE = 'T';
        private const char ARC = 'A';
        public static PathCommand CreateCommand(string commandString)
        {
            
            return null;
        }
        public bool IsAbsolute { get => isAbsolute; set => isAbsolute = value; }
    }
}
