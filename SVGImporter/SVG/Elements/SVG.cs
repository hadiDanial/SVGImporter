using SVGImporter.Utility;

namespace SVGImporter.SVG.Elements
{
    public class SVG : ParentElement
    {
        private Vector2 size;
        private ViewBox viewBox;

        public SVG(string tagText, List<Attribute> attributes, Vector2 size, ViewBox viewBox) : base(tagText, attributes)
        {
            this.size = size;
            this.viewBox = viewBox;
        }

        public static new ParentElement GetElement(string tagText)
        {
            return null;
        }

        public override string GetElementName()
        {
            return "SVG";
        }

        public override string GetElementNameReadable()
        {
            return "SVG";
        }
    }
}