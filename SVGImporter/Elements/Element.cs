using SVGImporter.Elements.Containers;
using System;
using System.Text.RegularExpressions;

namespace SVGImporter.Elements
{
    public abstract class Element
    {
        protected List<TagAttribute> attributes;
        protected Style style;
        protected string elementNameReadable, elementName, elementId;
        private static Dictionary<string, TagType> tagTypeStringToEnum = new Dictionary<string, TagType>();
        protected const string OPENING_TAG = "<";
        protected const string CLOSING_TAG = ">";
        protected const string SINGLE_TAG_PATTERN = "<\\w+( ([^<])*?)+? *?(\\/|\\\\)>";
        protected const string GROUP_TAG_PATTERN = "<(\\w+)( (\\r| |\\t|\\n|.)*?)+>((\\r| |\\t|\\n|.)*?)<(\\\\|\\/)\\1+>";
        protected Element(List<TagAttribute> attributes)
        {
            elementNameReadable = GetElementNameReadable();
            elementName = GetElementName(GetTagType());
            elementId = GetCustomName(attributes);
            this.attributes = attributes;
        }

        private string GetCustomName(List<TagAttribute> attributes)
        {
            string name = GetElementName(GetTagType());
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].attributeName.Equals("id"))
                    name = attributes[i].attributeValue;
            }
            return name;
        }


        internal static Element CreateElement(TagType type, string tagText, List<TagAttribute> attributes)
        {
            switch (type)
            {
                case TagType.Circle:
                    return new Circle(attributes);
                case TagType.Ellipse:
                    return new Ellipse(attributes);
                case TagType.G:
                    return new Containers.Group(attributes);
                case TagType.Line:
                    return new Line(attributes);
                case TagType.Path:
                    return new Path(attributes);
                case TagType.Polygon:
                    return new Polygon(attributes);
                case TagType.Polyline:
                    return new Polyline(attributes);
                case TagType.Rect:
                    return new Rect(attributes);
                case TagType.SVG:
                    return new SVG(attributes);
                case TagType.Style:
                    return new Style(attributes);
                case TagType.Unknown:
                default:
                    return new UnsupportedElement(attributes);

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
            tagTypeStringToEnum.Add("style", TagType.Style);
        }
        public static TagType GetTypeByName(string name)
        {
            if (tagTypeStringToEnum == null || tagTypeStringToEnum.Count == 0) SetupDictionary();
            return tagTypeStringToEnum[name];
        }
        /// <summary>
        /// Return the element name as per the SVG specification.
        /// </summary>
        public static string GetElementName(TagType tagType)
        {
            if (tagTypeStringToEnum == null || tagTypeStringToEnum.Count == 0) SetupDictionary();
            foreach (string key in tagTypeStringToEnum.Keys)
            {
                if (tagTypeStringToEnum[key] == tagType) return key;
            }
            return "Element";
        }
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


        public static bool IsContainer(TagType type)
        {
            switch (type)
            {
                case TagType.Circle:
                case TagType.Ellipse:
                case TagType.Line:
                case TagType.Path:
                case TagType.Polygon:
                case TagType.Polyline:
                case TagType.Rect:
                    return false;
                case TagType.G:
                case TagType.SVG:
                case TagType.Style:
                case TagType.Unknown:
                default:
                    return true;
            }
        }


        /// <summary>
        /// Convert the attributes to SVG elements.
        /// </summary>
        /// <returns></returns>
        protected string AttributesToSVG()
        {
            return TagAttribute.AttributesToSVG(attributes);
        }

        protected abstract TagType GetTagType();

    }
}
