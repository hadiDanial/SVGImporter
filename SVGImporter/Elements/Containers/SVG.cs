using SVGImporter.Elements;
using SVGImporter.Utility;
using System.Text.RegularExpressions;

namespace SVGImporter.Elements.Containers
{
    public class SVG : ParentElement
    {
        private Vector2 size;
        private ViewBox viewBox;

        protected SVG(string tagText, List<TagAttribute> attributes, Vector2 size, ViewBox viewBox) : base(tagText, attributes)
        {
            this.size = size;
            this.viewBox = viewBox;
        }

        public static new SVG GetElement(string tagText)
        {
            string pattern = GetRegexPattern(GetElementName());
            Console.WriteLine(pattern);
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(tagText);
            foreach (Match match in matches)
            {
                Console.WriteLine("Match Complete:\n" + match.Value + "\n______\n");
            }
            regex = new Regex(GetOpeningTagRegexPattern(GetElementName()));
            matches = regex.Matches(tagText);
            if (matches.Count <= 0) throw new InvalidDataException($"No opening tag found for {GetElementName()}");

            string start = matches[0].Value;
            int index = tagText.IndexOf(start);
            string content = tagText.Substring(start.Length);
            regex = new Regex(GetClosingTagRegexPattern(GetElementName()));
            matches = regex.Matches(tagText);
            if (matches.Count <= 0) throw new InvalidDataException($"No closing tag found for {GetElementName()}");
            index = content.IndexOf(matches[0].Value);
            content = content.Substring(0, index);

            Console.WriteLine("Content:\n" + content);


            return null;
        }

        public new static string GetElementName()
        {
            return "svg";
        }

        public new static string GetElementNameReadable()
        {
            return "SVG";
        }
    }
}