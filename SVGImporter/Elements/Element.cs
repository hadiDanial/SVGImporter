using SVGImporter.Elements.Containers;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SVGImporter.Elements.Transforms;

namespace SVGImporter.Elements
{
    [Serializable]
    public abstract class Element
    {
        private List<TagAttribute> attributes;
        protected Style style;
        protected string elementNameReadable;
        private static Dictionary<string, TagType> tagTypeStringToEnum = new Dictionary<string, TagType>();
        private string elementName;
        private string elementId;
        protected const string OPENING_TAG = "<";
        protected const string CLOSING_TAG = ">";
        protected const string SINGLE_TAG_PATTERN = "<\\w+( ([^<])*?)+? *?(\\/|\\\\)>";
        protected const string GROUP_TAG_PATTERN = "<(\\w+)( (\\r| |\\t|\\n|.)*?)+>((\\r| |\\t|\\n|.)*?)<(\\\\|\\/)\\1+>";
        private SVGTransform transform;

        public static Dictionary<string, TagType> TagTypeStringToEnum
        {
            get {
                if (tagTypeStringToEnum == null || tagTypeStringToEnum.Count == 0) SetupDictionary();
                return tagTypeStringToEnum;
            }
        }

        public SVGTransform Transform => transform;

        public string ElementName { get => elementName; set => elementName = value; }
        public List<TagAttribute> Attributes { get => attributes; set => attributes = value; }
        public string ElementId { get => elementId; set => elementId = value; }

        protected Element(List<TagAttribute> attributes)
        {
            elementNameReadable = GetElementNameReadable();
            elementName = GetElementName(GetTagType());
            elementId = GetCustomName(attributes);
            this.attributes = attributes;
            GenerateTransform();
        }

        /// <summary>
        /// Parses the transformation attributes of the element and generates the corresponding transformation operations.
        /// </summary>
        private void GenerateTransform()
        {
            transform = new SVGTransform();
            foreach (TagAttribute attribute in this.attributes)
            {
                if (attribute.attributeName.Equals("transform"))
                {
                    string transformOptions = attribute.attributeValue;
                    Regex regex = new Regex(@"(\w+)\s*\(\s*([^\)]+)\s*\)");
                    MatchCollection matches = regex.Matches(transformOptions);

                    foreach (Match match in matches)
                    {
                        string functionName = match.Groups[1].Value;
                        string argumentString = match.Groups[2].Value;

                        // Split the argument string into individual arguments
                        string[] arguments = argumentString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Process the transform function based on its name and arguments
                        switch (functionName)
                        {
                            case "translate":
                            {
                                // Process a translation transform with arguments (tx, ty)
                                float tx = float.Parse(arguments[0]);
                                float ty = arguments.Length > 1 ? float.Parse(arguments[1]) : 0.0f;
                                transform.AddOperation(new TranslateOperation(tx, ty));
                                break;
                            }

                            case "rotate":
                            {
                                // Process a rotation transform with arguments (angle, cx, cy)
                                float angle = float.Parse(arguments[0]);
                                float cx = arguments.Length > 1 ? float.Parse(arguments[1]) : 0.0f;
                                float cy = arguments.Length > 2 ? float.Parse(arguments[2]) : 0.0f;
                                transform.AddOperation(new RotateOperation(angle, cx, cy));
                                break;
                            }

                            case "scale":
                            {
                                // Process a scale transform with arguments (sx, sy)
                                float sx = float.Parse(arguments[0]);
                                float sy = arguments.Length > 1 ? float.Parse(arguments[1]) : sx;
                                transform.AddOperation(new ScaleOperation(sx, sy));
                                break;
                            }

                            case "matrix":
                            {
                                // Process a matrix transform with arguments (a, b, c, d, e, f)
                                float a = float.Parse(arguments[0]);
                                float b = float.Parse(arguments[1]);
                                float c = float.Parse(arguments[2]);
                                float d = float.Parse(arguments[3]);
                                float e = float.Parse(arguments[4]);
                                float f = float.Parse(arguments[5]);
                                transform.AddOperation(new MatrixOperation(a, b, c, d, e, f));
                                break;
                            }
                            case "skewX":
                            {
                                // Process a skewX transform with a single angle value in degrees
                                float skewXAngle = float.Parse(arguments[0]);
                                transform.AddOperation(new SkewOperation(skewXAngle, 0));
                                break;
                            }

                            case "skewY":
                            {                                
                                // Process a skewY transform with a single angle value in degrees
                                float skewYAngle = float.Parse(arguments[0]);
                                transform.AddOperation(new SkewOperation(0, skewYAngle));
                                break;
                            }
                        }
                    }
                }
            }
        }


        private string GetCustomName(List<TagAttribute> attributes)
        {
            string name = string.Empty;
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].attributeName.Equals("id"))
                    name = attributes[i].attributeValue;
            }
            return name;
        }

        /// <summary>
        /// Factory method that creates a new instance of a subclass of Element based on the specified TagType and attributes.
        /// </summary>
        internal static Element CreateElement(TagType type, List<TagAttribute> attributes, string localName)
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
                    return new UnsupportedElement(attributes, localName);

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
            if(TagTypeStringToEnum.ContainsKey(name))
                return tagTypeStringToEnum[name];
            return TagType.Unknown;
        }
        /// <summary>
        /// Return the element name as per the SVG specification.
        /// </summary>
        public static string GetElementName(TagType tagType)
        {
            foreach (string key in TagTypeStringToEnum.Keys)
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
        protected string AttributesToSVG(List<string> attributesToIgnore)
        {
            return TagAttribute.AttributesToSVG(attributes, attributesToIgnore);
        }

        public abstract TagType GetTagType();

    }
}
