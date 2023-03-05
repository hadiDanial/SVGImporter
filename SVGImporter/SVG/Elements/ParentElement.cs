using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    public abstract class ParentElement : Element
    {
        protected List<Element> children;

        public ParentElement(string tagText, ViewBox viewBox, List<Attribute> attributes) : base(tagText, viewBox, attributes)
        {
            children = new List<Element>();
        }

        public override string ElementToSVGTag()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetStartTag());
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
                stringBuilder.Append(element.GetElementNameReadable());
                stringBuilder.Append(", ");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
