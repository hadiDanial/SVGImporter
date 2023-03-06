using System.Text;

namespace SVGImporter.Elements.Containers
{
    public abstract class ParentElement : Element
    {
        protected List<Element> children;
        protected string value;
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
            return $"<{GetElementName()} {AttributesToSVG()}>";

        }
        protected string GetEndTag()
        {
            return $"{GetElementName()}";
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetElementNameReadable());
            stringBuilder.Append(": {");
            foreach (Element element in children)
            {
                stringBuilder.Append(GetElementNameReadable());
                stringBuilder.Append(", ");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        protected static string GetRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>((\r| |\t|\n|.)*?)<(\\\\|\\/){elementName}>";
        protected static string GetOpeningTagRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>";
        protected static string GetClosingTagRegexPattern(string elementName) => $"< *(\\\\|\\/) *{elementName} *>";
    }

}
