using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGImporter.SVG.Elements
{
    public abstract class Element
    {
        protected List<Attribute> attributes;
        protected Style style;
        protected string elementNameReadable, elementName;
        protected string tagText;
        protected string value;
        protected ViewBox viewBox;
      
        protected Element(string tagText, ViewBox viewBox, List<Attribute> attributes)
        {
            elementNameReadable = GetElementNameReadable();
            elementName = GetElementName();
            this.attributes = attributes;
            style = new Style();
            this.tagText = tagText;
            this.viewBox = viewBox;
        }


        public static Element CreateElement(string elementName, string tagText)
        {
            TagType type;
            if(Enum.TryParse<TagType>(elementName, true, out type))
            {
                Console.WriteLine($"{type}: {tagText}");
                return CreateElementByType(type, tagText);
            }
            else
            {
                type = TagType.Unknown;
                return new UnsupportedElement(tagText, null, null);
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

        /// <summary>
        /// Return the element name as per the SVG specification.
        /// </summary>
        public abstract string GetElementName();
        /// <summary>
        /// Return the element name in a readable form.
        /// </summary>
        /// <returns></returns>
        public abstract string GetElementNameReadable();
        
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
            return Attribute.AttributesToSVG(attributes);
        }
        
    }
}
