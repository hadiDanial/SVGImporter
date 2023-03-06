using System.Text;
using System.Text.RegularExpressions;

namespace SVGImporter.Elements.Containers
{
    public abstract class ParentElement : Element
    {
        protected List<Element> children;
        protected string value;
        public List<Element> Children => children;

        protected ParentElement(string tagText, List<TagAttribute> attributes) : base(tagText, attributes)
        {
            children = new List<Element>();
            value = string.Empty;
        }

        public override string ElementToSVGTag()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetStartTag());
            stringBuilder.Append(value);
            foreach (Element element in children)
            {
                stringBuilder.Append(element.ElementToSVGTag());
            }
            stringBuilder.Append(GetEndTag());
            return stringBuilder.ToString();
        }


        protected string GetStartTag()
        {
            return $"<{GetElementName(GetTagType())} {AttributesToSVG()}>";

        }

        protected string GetEndTag()
        {
            return $"</{GetElementName(GetTagType())}>";
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetElementNameReadable());
            stringBuilder.Append(": {");
            foreach (Element element in children)
            {
                stringBuilder.Append(element.ToString());
                stringBuilder.Append(", ");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        protected static string GetRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>((\r| |\t|\n|.)*?)<(\\\\|\\/){elementName}>";
        protected static string GetOpeningTagRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>";
        protected static string GetClosingTagRegexPattern(string elementName) => $"< *(\\\\|\\/) *{elementName} *>";

        protected static void GetContentAndAttributes(string tagText, string elementName, TagType tagType, out List<TagAttribute> attributes, out string content, out string value)
        {
            attributes = TagAttribute.SVGToAttributes(tagText);

            Regex regex = new Regex(GetOpeningTagRegexPattern(elementName));
            MatchCollection matches = regex.Matches(tagText);
            if (matches.Count <= 0) throw new InvalidDataException($"No opening tag found for {elementName}");

            string start = matches[0].Value;
            int index = tagText.IndexOf(start);
            content = tagText.Substring(start.Length);
            regex = new Regex(GetClosingTagRegexPattern(elementName));
            matches = regex.Matches(tagText);
            if (matches.Count <= 0) throw new InvalidDataException($"No closing tag found for {GetElementName(tagType)}");
            index = content.IndexOf(matches[0].Value);
            content = content.Substring(0, index);

            regex = new Regex($"^{GROUP_TAG_PATTERN}$");
            if(content.IndexOf(OPENING_TAG) == -1 && content.IndexOf(CLOSING_TAG) == -1)
            {
                value = content.Trim();
                return;
            }
            matches = regex.Matches(content);
            value = string.Empty;
            foreach (Match match in matches)
            {
                string v = match.Groups[4].Value.Trim();
                //if (match.Groups[0])
                MatchCollection secondaryMatches = regex.Matches(v);
                if (secondaryMatches.Count == 0)
                {
                    value += v;
                    //Console.WriteLine("Inside content group 4:\n" + v + "\n______\n");
                }
            }
        }
    }
}