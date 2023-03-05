using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter
{
    internal class SVGMain
    {
        static void Main(string[] args)
        {
            SVGFileParser.ReadFile("Resources/Line.svg");
        }
    }
}
