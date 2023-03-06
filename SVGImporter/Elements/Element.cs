using SVGImporter.Elements.Containers;

namespace SVGImporter.Elements
{
    public abstract class Element
    {
        protected List<TagAttribute> attributes;
        protected Style style;
        protected string elementNameReadable, elementName;
        protected string tagText;
        private static Dictionary<string, TagType> tagTypeStringToEnum = new Dictionary<string, TagType>();

        protected Element(string tagText, List<TagAttribute> attributes)
        {
            elementNameReadable = GetElementNameReadable();
            elementName = GetElementName(attributes);
            this.attributes = attributes;
            style = new Style();
            this.tagText = tagText;
        }

        private string GetElementName(List<TagAttribute> attributes)
        {
            string name = GetElementName();
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].attributeName.Equals("id"))
                    name = attributes[i].attributeValue;
            }
            return name;
        }

        public static Element CreateElement(string elementName, string tagText)
        {
            if (tagTypeStringToEnum == null || tagTypeStringToEnum.Count == 0) SetupDictionary();

            TagType type;
            if (Enum.TryParse(elementName, true, out type))
            {
                Console.WriteLine($"{type}: {tagText}");
                return CreateElementByType(type, tagText);
            }
            else
            {
                type = TagType.Unknown;
                return Group.GetElement(tagText);
            }
            return null;
        }

        private static Element CreateElementByType(TagType type, string tagText)
        {
            switch (type)
            {
                case TagType.Circle:
                    return Circle.GetElement(tagText);
                case TagType.Ellipse:
                    return Ellipse.GetElement(tagText);
                case TagType.G:
                    return Group.GetElement(tagText);
                case TagType.Line:
                    return Line.GetElement(tagText);
                case TagType.Path:
                    return Path.GetElement(tagText);
                case TagType.Polygon:
                    return Polygon.GetElement(tagText);
                case TagType.Polyline:
                    return Polyline.GetElement(tagText);
                case TagType.Rect:
                    return Rect.GetElement(tagText);
                case TagType.SVG:
                    return SVG.GetElement(tagText);

                case TagType.Unknown:
                default:
                    return UnsupportedElement.GetElement(tagText);

            }
        }
        private static void SetupDictionary()
        {
            tagTypeStringToEnum = new Dictionary<string, TagType>();
            tagTypeStringToEnum.Add("circle", TagType.Circle);
            tagTypeStringToEnum.Add("ellipse", TagType.Ellipse);
            tagTypeStringToEnum.Add("g", TagType.G);
            tagTypeStringToEnum.Add("line", TagType.Line);
            tagTypeStringToEnum.Add("path", TagType.Path);
            tagTypeStringToEnum.Add("polygon", TagType.Polygon);
            tagTypeStringToEnum.Add("polyline", TagType.Polyline);
            tagTypeStringToEnum.Add("rect", TagType.Rect);
            tagTypeStringToEnum.Add("svg", TagType.SVG);
        }
        /// <summary>
        /// Return the element name as per the SVG specification.
        /// </summary>
        public static string GetElementName() => "Element";
        /// <summary>
        /// Return the element name in a readable form.
        /// </summary>
        /// <returns></returns>
        public static string GetElementNameReadable() => "Element";

        /// <summary>
        /// Convert the element back to an SVG element.
        /// </summary>
        public abstract string ElementToSVGTag();
        public override abstract string ToString();
        public static Element GetElement(string tagText) => null;
        /// <summary>
        /// Convert the attributes to SVG elements.
        /// </summary>
        /// <returns></returns>
        protected string AttributesToSVG()
        {
            return TagAttribute.AttributesToSVG(attributes);
        }

    }
}
