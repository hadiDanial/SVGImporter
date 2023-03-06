using SVGImporter.Elements;
using SVGImporter.Elements.Containers;

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
            ReadSVG(svg);
        }
        public static string GetLocalPath(string path)
        {
            return System.IO.Path.Combine(Environment.CurrentDirectory, @path);
        }

        public static void ReadSVG(string svgText)
        {
            svgText = svgText.Replace("\r\n", string.Empty);

            int start = svgText.IndexOf("<svg");
            if (start == -1)
            {
                throw new InvalidDataException("No <svg> tag found!");
            }
            svgText = svgText.Substring(start);
            SVG svgElement = (SVG)Element.GetElement(svgText);

            Console.WriteLine("\n\nSVG:\n");
            foreach (Element element in svgElement.Children)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("Element To SVG:\n");
            Console.WriteLine(svgElement.ElementToSVGTag());

            //List<Element> elements = new List<Element>();
            //string[] lines = svgText.Split(CLOSING_TAG);
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    lines[i] += ">";
            //    int index = lines[i].IndexOf(" ");
            //    if(index == -1)
            //    {
            //        Console.WriteLine($"NO TAG LINE: {lines[i]}");
            //        continue;
            //    }
            //    string type = lines[i].Substring(lines[i].IndexOf(OPENING_TAG) + 1, index).Trim();
            //    Element element = Element.CreateElement(type, lines[i]);
            //    elements.Add(element);
            //    Console.WriteLine(element + " - DONE");
            //}

            //svg = svg.Substring(svg.IndexOf(svgStart));
            //svg = svg.Substring(svg.IndexOf(">") + 1);
            //string line = svg.Substring(svg.IndexOf("<line"));
            //line = svg.Substring(svg.IndexOf("/>"));
        }
    }
}
