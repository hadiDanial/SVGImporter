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
  
        public static Element? ReadSVGFile(string path, bool isLocalPath = true)
        {
            if (path == null || !path.ToLower().EndsWith(EXTENSION)) return null;
            if(isLocalPath) path = GetLocalPath(path);
            string svg = File.ReadAllText(path);
            return ReadSVG(svg);
        }
        public static string GetLocalPath(string path)
        {
            return System.IO.Path.Combine(Environment.CurrentDirectory, @path);
        }

        public static Element ReadSVG(string svgText)
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
            Element svg = ParseDocumentRecusrively(root);
            return svg;
        }

        private static Element ParseDocumentRecusrively(XElement root)
        {
            TagType type = Element.GetTypeByName(root.Name.LocalName);
            List<TagAttribute> attributes = TagAttribute.FromXElementAttributes(root.Attributes());
            Element createdElement = Element.CreateElement(type, attributes, root.Name.LocalName);
            if (root.HasElements)
            {
                List<Element> elementsList = new List<Element>();
                var elements = root.Elements();
                foreach (var element in elements)
                {
                    elementsList.Add(ParseDocumentRecusrively(element));
                }
                ((ParentElement)createdElement).SetChildren(elementsList);
            }
            else if(root.FirstNode != null && Element.IsContainer(type))
            {
                string value = root.FirstNode.ToString();
                ((ParentElement)createdElement).SetValue(value);
            }

            return createdElement;
        }

        public static void SaveFile(string generatedSVG, string path, bool isLocalPath = true)
        {
            if (path == null || !path.ToLower().EndsWith(EXTENSION)) return;
            if (isLocalPath) path = GetLocalPath(path);
            File.WriteAllText(path.Insert(path.IndexOf(EXTENSION),"_NEW"), generatedSVG);            
        }

        public static void SaveSVG(Element svg, string path, bool isLocalPath = true)
        {
            if (svg == null) return;
            SaveFile(svg.ElementToSVGTag(), path, isLocalPath);
        }
    }
}
