using SVGImporter.SVG.Elements;
using SVGImporter.Utility;

namespace SVGImporter.SVG
{
    public class SVG : ParentElement
    {
        private Vector2 size;

        public SVG(string tagText, ViewBox viewBox, List<Attribute> attributes, Vector2 size) : base(tagText, viewBox, attributes)
        {
            this.size = size;
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