using SVGImporter.Elements;
using SVGImporter.Elements.Containers;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

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
            var doc = XDocument.Parse(svgText);
            XElement root = doc.Root;
            XNamespace xNamespace = root.GetDefaultNamespace();
            Element svg = ParseDocumentRecusrively(root, xNamespace);
            SaveFile(svg.ElementToSVGTag(), path);
            //Console.WriteLine(svg.ElementToSVGTag());
        }

        private static Element ParseDocumentRecusrively(XElement root, XNamespace xNamespace)
        {
            TagType type = Element.GetTypeByName(root.Name.LocalName);
            List<TagAttribute> attributes = TagAttribute.FromXElementAttributes(root.Attributes());
            Element createdElement = Element.CreateElement(type, root.Value, attributes);
            if (root.HasElements)
            {
                List<Element> elementsList = new List<Element>();
                var elements = root.Elements();
                foreach (var element in elements)
                {
                    elementsList.Add(ParseDocumentRecusrively(element, xNamespace));
                }
                ((ParentElement)createdElement).SetChildren(elementsList);
            }

            return createdElement;
        }

        public static void SaveFile(string generatedSVG, string path, bool localPath = true)
        {
            if (path == null || !path.ToLower().EndsWith(EXTENSION)) return;
            if (localPath) path = GetLocalPath(path);
            File.WriteAllText(path.Insert(path.IndexOf(EXTENSION),"_NEW"), generatedSVG);            
        }
    }
}
