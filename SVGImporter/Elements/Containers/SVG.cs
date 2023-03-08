using SVGImporter.Elements;
using SVGImporter.Utility;
using System.Text.RegularExpressions;

namespace SVGImporter.Elements.Containers
{
    public class SVG : ParentElement
    {
        private Vector2 size;
        private ViewBox viewBox;

        internal SVG(List<TagAttribute> attributes) : base(attributes)
        {
            ViewBox viewBox = new ViewBox();
            float width = 0, height = 0, vWidth = 0, vHeight = 0, minX = 0, minY = 0, val;
            foreach (TagAttribute attribute in attributes)
            {
                if (attribute.attributeName.Equals("width") && float.TryParse(attribute.attributeValue, out val))
                    width = val;
                if (attribute.attributeName.Equals("height") && float.TryParse(attribute.attributeValue, out val))
                    height = val;
                if (attribute.attributeName.Equals("viewBox"))
                {
                    string[] values = attribute.attributeValue.Split(' ');
                    if (values == null || values.Length != 4)
                        throw new SVGException($"Invalid ViewBox values for {GetElementName(TagType.SVG)}");
                    if (float.TryParse(attribute.attributeValue, out val))
                        minX = val;
                    if (float.TryParse(attribute.attributeValue, out val))
                        minY = val;
                    if (float.TryParse(attribute.attributeValue, out val))
                        vWidth = val;
                    if (float.TryParse(attribute.attributeValue, out val))
                        vHeight = val;
                    viewBox = new ViewBox(new Vector2(vWidth, vHeight), new Vector2(minX, minY));
                }
            }
            this.viewBox = viewBox;
            this.size = new Vector2(width, height);
        }

        public override string ElementToSVGTag()
        {
            return base.ElementToSVGTag();
        }

        public new static string GetElementNameReadable()
        {
            return "SVG";
        }

        protected override TagType GetTagType()
        {
            return TagType.SVG;
        }
    }
}