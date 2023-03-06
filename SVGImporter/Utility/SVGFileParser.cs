using SVGImporter.Elements;
using SVGImporter.Elements.Containers;
using System.ComponentModel;
using System.IO;

namespace SVGImporter.Utility
{
    internal class SVGFileParser
    {
        private const string EXTENSION = ".svg";
        private const string OPENING_TAG = "<";
        private const string CLOSING_TAG = ">";
  
        public static void ReadFile(string path, bool localPath = true)
        {
            if (path == null || !path.ToLower().EndsWith(EXTENSION)) return;
            if(localPath) path = GetLocalPath(path);
            string svg = File.ReadAllText(path);
            ReadSVG(svg, path);
        }
        public static string GetLocalPath(string path)
        {
            return System.IO.Path.Combine(Environment.CurrentDirectory, @path);
        }

        public static void ReadSVG(string svgText, string path)
        {
            svgText = svgText.Replace("\r\n", string.Empty);

            int start = svgText.IndexOf("<svg");
            if (start == -1)
            {
                throw new InvalidDataException("No <svg> tag found!");
            }
            svgText = svgText.Substring(start);
            SVG svgElement = (SVG)Element.GetElement(svgText);

            Console.WriteLine("Element To SVG:\n");
            Console.WriteLine(svgElement.ElementToSVGTag());
            SaveFile(svgElement.ElementToSVGTag(), path);
        }

        public static void SaveFile(string generatedSVG, string path, bool localPath = true)
        {
            if (path == null || !path.ToLower().EndsWith(EXTENSION)) return;
            if (localPath) path = GetLocalPath(path);
            File.WriteAllText(path.Insert(path.IndexOf(EXTENSION),"_NEW"), generatedSVG);            
        }
    }
}
