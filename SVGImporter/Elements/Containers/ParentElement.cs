using System.Text;
using System.Text.RegularExpressions;

namespace SVGImporter.Elements.Containers
{
    public abstract class ParentElement : Element
    {
        protected List<Element> children;
        protected string value;
        public List<Element> Children => children;

        protected ParentElement(List<TagAttribute> attributes) : base(attributes)
        {
            children = new List<Element>();
            value = string.Empty;
        }

        public override string ElementToSVGTag()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetStartTag());
            //stringBuilder.Append('\n');
            stringBuilder.Append(value);
            //stringBuilder.Append('\n');

            foreach (Element element in children)
            {
                stringBuilder.Append(element.ElementToSVGTag());
                //stringBuilder.Append('\n');
            }
            stringBuilder.Append(GetEndTag());
            //stringBuilder.Append('\n');
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
            stringBuilder.Append(GetElementName(GetTagType()));
            if(!string.IsNullOrEmpty(value))
            {
                stringBuilder.Append($"(value: {value})");
            }
            if (children == null || children.Count == 0) return stringBuilder.ToString();
            stringBuilder.Append(": {\n");

            foreach (Element element in children)
            {
                stringBuilder.Append("\t");
                stringBuilder.Append(element.ToString());
                stringBuilder.Append(",\n");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("\n}");
            return stringBuilder.ToString();
        }

        protected static string GetRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>((\r| |\t|\n|.)*?)<(\\\\|\\/){elementName}>";
        protected static string GetOpeningTagRegexPattern(string elementName) => $"<{elementName}( (\r| |\t|\n|.)*?)+>";
        protected static string GetClosingTagRegexPattern(string elementName) => $"< *(\\\\|\\/) *{elementName} *>";

        internal void SetChildren(List<Element> elementsList)
        {
            this.children = elementsList;
        }

        internal void SetValue(string value)
        {
            this.value = value.Replace("\t", string.Empty).Trim();
        }
    }
}