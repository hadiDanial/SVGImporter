using SVGImporter.Elements;
using SVGImporter.Utility;

namespace SVGImporter
{
    internal class SVGMain
    {
        static void Main(string[] args)
        {
            Element element = SVGFileParser.ReadSVGFile("Resources/NewTux.svg");
            Console.WriteLine(element.ElementToSVGTag());
            Console.WriteLine("\n\n\n");
            Console.WriteLine(element.ToString());
            SVGFileParser.SaveSVG(element, "Resources/NewTux.svg");
        }
    }
}
