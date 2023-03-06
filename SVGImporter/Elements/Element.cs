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
        protected string tagText;
        private static Dictionary<string, TagType> tagTypeStringToEnum = new Dictionary<string, TagType>();
        protected const string OPENING_TAG = "<";
        protected const string CLOSING_TAG = ">";
        protected const string SINGLE_TAG_PATTERN = "<\\w+( ([^<])*?)+? *?(\\/|\\\\)>";
        protected const string GROUP_TAG_PATTERN = "<(\\w+)( (\\r| |\\t|\\n|.)*?)+>((\\r| |\\t|\\n|.)*?)<(\\\\|\\/)\\1+>";
        protected Element(string tagText, List<TagAttribute> attributes)
        {
            elementNameReadable = GetElementNameReadable();
            elementName = GetElementName(GetTagType());
            elementId = GetCustomName(attributes);
            this.attributes = attributes;
            this.tagText = tagText;
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


        private static Element CreateElementByType(TagType type, string tagText)
        {
            switch (type)
            {
                case TagType.Circle:
                    return Circle.GetElement(tagText);
                case TagType.Ellipse:
                    return Ellipse.GetElement(tagText);
                case TagType.G:
                    return Containers.Group.GetElement(tagText);
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
                case TagType.Style:
                    return Style.GetElement(tagText);
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

        /// <summary>
        /// Create an element from a given SVG tag.
        /// </summary>
        /// <param name="tagText">A single SVG tag, which may or may not include content.</param>
        public static Element GetElement(string tagText)
        {
            if (tagTypeStringToEnum == null || tagTypeStringToEnum.Count == 0) SetupDictionary();
            tagText = tagText.Trim();

            int firstSpaceIndex = tagText.IndexOf(' ');
            int firstClosingTagIndex = tagText.IndexOf(OPENING_TAG) + 1;
            string elementName = tagText.Substring(firstClosingTagIndex, firstSpaceIndex).Trim();
            TagType type;
            if (Enum.TryParse(elementName, true, out type))
            {
                Console.WriteLine($"{type}: {tagText}");
                return CreateElementByType(type, tagText);
            }
            else
            {
                type = TagType.Unknown;
                Console.WriteLine($"NA: {type}: {tagText}");
                return UnsupportedElement.GetElement(tagText);
            }
        }


        /// <summary>
        /// Get a list of all elements contained in the content.
        /// </summary>
        /// <param name="content">A string containing several SVG tags.</param>
        /// <returns>List of elements</returns>
        protected static List<Element> GetElements(string content)
        {
            content = content.Replace("\n",string.Empty).Trim(' ');
            string elementName, firstNameInContent;
            TagType type;
            List<Element> elements = new List<Element>();
            Regex regex = new Regex(SINGLE_TAG_PATTERN);
            MatchCollection matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                string text = match.Value;
                //if (!content.Trim().StartsWith(text.Trim())) continue;
                firstNameInContent = GetElementNameFromText(content);
                elementName = GetElementNameFromText(text);
                Enum.TryParse(firstNameInContent, true, out type);
                if (IsContainer(type) && !elementName.Equals(firstNameInContent) && IsInContainer(content, text, type)) continue;
                if (Enum.TryParse(elementName, true, out type))
                {
                    Console.WriteLine($"{type}: {text}");
                    elements.Add(CreateElementByType(type, text));
                }
                else
                {
                    type = TagType.Unknown;
                    elements.Add(UnsupportedElement.GetElement(text));
                }
                content = content.Replace(text, string.Empty).Trim(' ');
            }
            regex = new Regex(GROUP_TAG_PATTERN);
            content = content.Trim();

            //matches = regex.Matches(content);
            elementName = GetElementNameFromText(content);

            if (Enum.TryParse(elementName, true, out type))
            {
                if (IsContainer(type))
                    elements.Add(CreateElementByType(type, GetContainerContent(content, type)));
            }
            //foreach (Match match in matchesList)
            //{
            //    elements.Add(Element.CreateElementByType(GetTypeByName(match.Groups[1].Value), match.Value));

            //}

            return elements;
        }

        private static bool IsInContainer(string content, string text, TagType type)
        {
            int containerEndIndex = content.IndexOf($"</{GetElementName(type)}");
            int textStartIndex = content.IndexOf(text);
            return textStartIndex < containerEndIndex;
        }

        private static bool IsContainer(TagType type)
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

        private static string GetContainerContent(string content, TagType type)
        {
            string name = Element.GetElementName(type);
            int firstIndexName = content.IndexOf($"<{name}");
            int firstIndexClosing = content.IndexOf(">",firstIndexName);
            int lastIndexName = content.IndexOf($"/{name}", firstIndexClosing);
            return content.Substring(firstIndexClosing, lastIndexName - firstIndexClosing - 1);
        }

        private static string GetElementNameFromText(string text)
        {
            int firstOpeningTag = text.IndexOf(OPENING_TAG) + 1;
            int firstSpaceIndex = text.IndexOf(' ', firstOpeningTag);
            if (firstSpaceIndex == -1 || firstOpeningTag == -1) return text;
            string elementName = text.Substring(firstOpeningTag, firstSpaceIndex).Trim();
            return elementName;
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
