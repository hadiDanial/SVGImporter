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
            return null;
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
        
        /// <summary>
        /// Convert the attributes to SVG elements.
        /// </summary>
        /// <returns></returns>
        protected string AttributesToSVG()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Attribute attribute in attributes)
            {
                stringBuilder.Append(attribute);
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }
        
    }
}
